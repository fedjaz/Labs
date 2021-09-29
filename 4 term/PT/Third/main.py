import json
import multiprocessing
import os
import random
import string
from json import load
from os import mkdir
import requests
from app.telegram_config import telegram_token, telegram_url, admin_id, url, cores
from app.models import *
from app.messages import *
import zipfile
from flask import request
from tester_lib.tester import Tester
from tester_lib.models.test import Test as TesterLibTest
from tester_lib.models.task import Task as TesterLibTask
from tester_lib.models.code import Code as TesterLibCode, Compiler as TesterLibCompiler
from multiprocessing.queues import Queue
from multiprocessing import Process
from app import flask_app, db


is_testing = False
testing_last_message = ""
testing_downloding_file = None
testing_is_running = False

tests_queue = Queue(100, ctx=multiprocessing.get_context())


default_contest = Contest.query.filter(Contest.join_key == "AAAA-AAAA-AAAA").first()
if default_contest is None:
    admin_user = User(id=int(admin_id))
    db.session.add(admin_user)
    default_contest = Contest(admin_id=admin_id,
                              id=1,
                              name="Fedjaz Contest",
                              join_key="AAAA-AAAA-AAAA")
    db.session.add(default_contest)
    db.session.commit()


@flask_app.route("/", methods=["POST"])
def home():
    if is_testing:
        return "OK"
    try:
        id = request.json["message"]["from"]["id"]
        username = request.json["message"]["from"]["username"]
    except:
        return "OK"
    if "message" in request.json:
        if "text" in request.json["message"]:
            message = request.json["message"]["text"]
        else:
            message = ""
        if "document" in request.json["message"] and "file_id" in request.json["message"]["document"]:
            file_id = request.json["message"]["document"]["file_id"]
        else:
            file_id = ""
    else:
        message = ""
        file_id = ""

    proceed_message(id, username, message, file_id)
    return "OK"


def proceed_message(id, username, message, file_id):

    user = User.query.get(id)
    if user is None:
        new_user = User(id=id,
                        username=username,
                        selected_compiler="cpp",
                        active_contest_id=default_contest.id)

        db.session.add(new_user)
        db.session.commit()

        new_relation = UsersContestsRelation(user_id=id,
                                             contest_id=default_contest.id)
        db.session.add(new_relation)
        db.session.commit()
        send_message(id, greeting_message)
        return
    elif user.username != username:
        user.username = username
        user.selected_compiler = "cpp"
        user.active_contest_id = default_contest.id
        db.session.commit()
        new_relation = UsersContestsRelation(user_id=id,
                                             contest_id=default_contest.id)
        db.session.add(new_relation)
        db.session.commit()
        return

    if re.match(command_cancel_re, message):
        if user.last_command is None:
            send_message(id, cancel_error_message)
        else:
            send_message(id, cancel_message.format(user.last_command))
            user.last_command = None
            user.state = None
            db.session.commit()
        return
    elif user.last_command == command_createtask:
        if file_id != "":
            file = download_file(file_id)
            tests = []
            task = Task()
            try:
                file = zipfile.ZipFile(file, "r")
                list = file.infolist()
                list.sort(key=lambda x: x.filename)
                for i in list:
                    name = i.filename.split('/', 1)[1]
                    if i.is_dir() or '.a' in i.filename:
                        continue

                    if name == "settings.json":
                        json = load(file.open(i.filename))
                        task.name = json["name"]
                        res = Task.query.filter(Task.name == task.name)\
                            .filter(Task.contest_id == user.active_contest_id)\
                            .first()
                        if res is not None:
                            send_message(id, task_name_occupied_error_message)
                            return
                        task.statement = json["statement"]
                        task.time_limit = int(json["time_limit"])
                        if task.time_limit < 0 or task.time_limit > 3000:
                            send_message(id, task_tl_error_message)
                            return
                        task.memory_limit = int(json["memory_limit"])
                        if task.memory_limit < 0 or task.memory_limit > 1024:
                            send_message(id, task_ml_error_message)
                            return
                        task.contest_id = user.active_contest_id

                    elif re.match(r"^tests/(\d+)$", name):
                        test_number = int(name.split("/")[1])
                        input_string = file.open(i.filename).read().decode("utf-8")
                        output_string = file.open(f"{i.filename}.a").read().decode("utf-8")
                        if test_number != len(tests) + 1:
                            raise ValueError
                        test = Test(index=test_number,
                                    input_string=input_string,
                                    output_string=output_string)
                        tests.append(test)
                    else:
                        raise ValueError

                if len(tests) > 50:
                    send_message(id, task_tests_too_many_error_message)
                    return
                if len(tests) == 0:
                    send_message(id, task_no_tests_error_message)
                    return
                db.session.add(task)
                db.session.commit()
                for i in tests:
                    i.task_id = task.id
                    db.session.add(i)
                db.session.commit()

                send_message(id, task_success_message.format(task.name))
                user.last_command = None
                db.session.commit()

            except Exception as ex:
                send_message(id, task_file_format_error_message)
                return

        else:
            send_message(id, task_file_error_message)
        return
    elif user.last_command == command_submit:
        if is_testing:
            global testing_is_running
            testing_is_running = True

        if file_id != "":
            try:
                file = download_file(file_id)
                code = open(file, "r").read()
            except:
                send_message(id, submit_file_error_message)
                return
        else:
            code = message

        send_message(id, submit_wait_message)
        active_contest = Contest.query.get(user.active_contest_id)
        contest = active_contest
        task = contest.tasks[int(user.state)]

        user.last_command = command_tasks
        db.session.commit()
        test_solution(task, code, user.selected_compiler, user.id)

    elif re.match(command_help_re, message):
        send_message(id, help_message)
    elif re.match(command_createtask_re, message):
        active_contest = Contest.query.get(user.active_contest_id)
        if active_contest.admin_id != user.id:
            send_message(id, task_not_admin_error_message)
        else:
            send_message(id, createtask_message)
            user.last_command = message
            db.session.commit()
    elif re.match(command_tasks_re, message):
        active_contest = Contest.query.get(user.active_contest_id)
        solutions = Solution.query.filter(Solution.user_id == user.id)\
            .filter(Solution.is_latest)
        tasks = active_contest.tasks
        task_list = ""
        for i in range(0, len(tasks)):
            solution = solutions.filter(Solution.task_id == tasks[i].id).first()
            if solution is not None:
                if solution.test_result == "OK":
                    res = tasks_ok_message
                else:
                    res = tasks_not_ok_message
            else:
                res = ""
            task_list += f"{i + 1}) {tasks[i].name}{res}\n"
        if task_list == "":
            task_list = tasks_empty_message
        send_message(id, task_list)
    elif re.match(command_select_re, message):
        active_contest = Contest.query.get(user.active_contest_id)

        tasks = active_contest.tasks
        index = int(message.split(' ')[1])
        if 0 < index <= len(tasks):
            task = tasks[index - 1]
            user.last_command = message.split(' ')[0]
            user.state = str(index - 1)
            db.session.commit()
            send_message(id, str(task))
        else:
            send_message(id, select_wrong_index_error_message)
    elif re.match(command_submit_re, message):
        if user.state is None:
            send_message(id, submit_no_task_selected_error_message)
        else:
            user.last_command = message
            send_message(id, submit_message)
            db.session.commit()
    elif re.match(command_compiler_re, message):
        compiler = message.split(' ')[1]
        if compiler == "py" or compiler == "cpp":
            user.selected_compiler = compiler
            db.session.commit()
            send_message(id, compiler_set_message.format(compiler))
        else:
            send_message(id, compiler_error_message.format(compiler))
    elif re.match(command_standings_re, message):
        active_contest = Contest.query.get(user.active_contest_id)

        contest = active_contest
        tasks = contest.tasks
        list = ""
        user_dict = {}
        for task in tasks:
            solutions = Solution.query.filter(Solution.task_id == task.id)\
                .filter(Solution.is_latest)\
                .filter(Solution.test_result == "OK").all()
            for solution in solutions:
                if solution.user_id not in user_dict:
                    user_dict[solution.user_id] = 0
                user_dict[solution.user_id] += 1
        srt = [(key, user_dict[key]) for key in user_dict]
        srt.sort(key=lambda x: x[1])
        for (key, value) in srt:
            user = User.query.get(key)
            list += f"@{user.username}({user.id}) - {value}/{len(tasks)}\n"

        send_message(id, standings_message.format(contest.name, list))
    elif re.match(command_userstats_re, message):
        user_id = int(message.split(' ')[1])
        active_contest = Contest.query.get(user.active_contest_id)

        relation = UsersContestsRelation.query.filter(UsersContestsRelation.user_id == user_id)\
            .filter(UsersContestsRelation.contest_id == active_contest.id)\
            .first()
        if relation is not None:
            user_full = User.query.get(user_id)
            tasks = active_contest.tasks
            list = ""
            index = 0
            for task in tasks:
                index += 1
                solution = Solution.query.filter(Solution.user_id == user_full.id)\
                    .filter(Solution.task_id == task.id)\
                    .filter(Solution.is_latest)\
                    .first()
                res = ""
                if solution is not None:
                    if solution.test_result == "OK":
                        res = tasks_ok_message
                    else:
                        res = tasks_not_ok_message
                list += f"{index}) {task.name}{res}\n"
            send_message(id, userstats_message.format(f"@{user_full.username}({user_full.id})",
                                                      active_contest.name,
                                                      list))

        else:
            send_message(id, userstats_no_user_error_message)
    elif re.match(command_contests_re, message):
        relations = UsersContestsRelation.query\
            .filter(UsersContestsRelation.user_id == user.id)\
            .all()
        list = ""
        for relation in relations:
            contest = Contest.query.get(relation.contest_id)
            list += f"{contest.name} - {contest.join_key}\n"
        send_message(id, contests_message.format(list))
    elif re.match(command_createcontest_re, message):
        name = message.split(' ', 1)[1]

        contest = 0
        code = ""
        while contest is not None:
            code = create_code()
            contest = Contest.query.filter(Contest.join_key == code).first()

        contest = Contest(admin_id=user.id,
                          name=name,
                          join_key=code)
        db.session.add(contest)
        db.session.commit()
        new_relation = UsersContestsRelation(user_id=user.id, contest_id=contest.id)
        db.session.add(new_relation)
        user.active_contest_id = contest.id
        user.last_command = None
        user.state = None
        db.session.commit()
        send_message(id, createcontest_message.format(contest.name, contest.join_key))

    elif re.match(command_contestinfo_re, message):
        active_contest = Contest.query.get(user.active_contest_id)

        contest = active_contest
        name = contest.name
        code = contest.join_key

        relations = UsersContestsRelation.query\
            .filter(UsersContestsRelation.contest_id == contest.id)\
            .all()

        users_count = len(relations)
        tasks_count = len(contest.tasks)
        admin = User.query.get(contest.admin_id)
        admin = f"@{admin.username}({admin.id})"
        list = ""
        index = 0
        for relation in relations:
            index += 1
            user = User.query.get(relation.user_id)
            list += f"{index}) @{user.username}({user.id})\n"
        send_message(id, contests_info_message.format(name,
                                                      code,
                                                      users_count,
                                                      tasks_count,
                                                      admin,
                                                      list))
    elif re.match(command_join_re, message):
        code = message.split(' ')[1]
        contest = Contest.query.filter(Contest.join_key == code).first()
        if contest is None:
            send_message(id, joincontest_no_contest_error_message)
        else:
            user.last_command = None
            user.state = None
            user.active_contest_id = contest.id

            relation = UsersContestsRelation.query\
                .filter(UsersContestsRelation.user_id == user.id)\
                .filter(UsersContestsRelation.contest_id == contest.id)\
                .first()
            if relation is None:
                new_relation = UsersContestsRelation(user_id=user.id,
                                                     contest_id=contest.id)
                db.session.add(new_relation)

            db.session.commit()
            send_message(id, joincontest_message.format(contest.name))
    elif re.match(command_mycontests_re, message):
        contests = Contest.query.filter(Contest.admin_id == user.id).all()
        if not contests:
            send_message(id, mycontests_no_contests_error_message)
        else:
            list = ""
            for contest in contests:
                list += f"{contest.name} - {contest.join_key}\n"
            send_message(id, mycontests_message.format(list))
    elif re.match(command_deletetask_re, message):
        contest = Contest.query.get(user.active_contest_id)
        if contest.admin_id != user.id and user.id != admin_id:
            send_message(id, deletetask_not_admin_error_message)
        else:
            index = int(user.state)
            if index is not None:
                task = contest.tasks[index]
                name = task.name
                users = User.query.filter(User.active_contest_id == contest.id).all()
                for user_full in users:
                    user_full.last_command = None
                    user_full.state = None
                    db.session.commit()
                db.session.delete(task)
                db.session.commit()
                send_message(id, deletetask_message.format(name))
            else:
                send_message(id, submit_no_task_selected_error_message)

    elif re.match(command_deletecontest_re, message):
        delete_contest(user, Contest.query.get(user.active_contest_id))
    elif re.match(admin_command_allcontests_re, message) and user.id == admin_id:
        contests = Contest.query.all()
        list = ""
        for contest in contests:
            list += f"{contest.name} - {contest.join_key}\n"
        send_message(id, admin_allcontests_message.format(list))
    else:
        send_message(id, command_error_message)

    return


def delete_contest(user, contest):
    if contest.admin_id != user.id and user.id != admin_id:
        send_message(user.id, deletecontest_not_admin_error_message)
    elif contest.join_key == "AAAA-AAAA-AAAA":
        send_message(user.id, deletecontest_default_contest_error_message)
    else:
        relations = UsersContestsRelation.query \
            .filter(UsersContestsRelation.contest_id == contest.id) \
            .all()
        for relation in relations:
            user_info = User.query.get(relation.user_id)
            user_info.last_command = None
            user_info.state = None
            user_info.active_contest_id = 1
            db.session.delete(relation)
            db.session.commit()
        for task in contest.tasks:
            db.session.delete(task)
            db.session.commit()
        send_message(user.id, deletecontest_message.format(contest.name))
        db.session.delete(contest)
        db.session.commit()


def commit_solution(report, code, compiler, user_id, task_id):
    user = User.query.get(user_id)
    db.session.query(User).get(user_id)
    latest_solution = Solution.query.filter(Solution.user_id == user_id) \
        .filter(Solution.task_id == task_id) \
        .filter(Solution.is_latest).first()

    if latest_solution is not None:
        latest_solution.is_latest = False

    solution = Solution(task_id=task_id,
                        user_id=user_id,
                        is_latest=True,
                        code=code,
                        compiler=compiler,
                        test_result=report.total_result.name,
                        full_report=str(report))
    if len(solution.full_report) <= 4096:
        send_message(user.id, solution.full_report)
        db.session.add(solution)
    else:
        send_message(user.id, solution.test_result)

    db.session.add(solution)
    db.session.commit()

    if is_testing:
        global testing_is_running
        testing_is_running = False


def test_solution(task, code, compiler, user_id):
    raw_tests = []
    for i in task.tests:
        raw_test = TesterLibTest(i.input_string, i.output_string)
        raw_tests.append(raw_test)

    raw_task = TesterLibTask(raw_tests, task.time_limit, task.memory_limit)
    raw_code = TesterLibCode(code, TesterLibCompiler[compiler])
    solution_id = f"solution_{task.id}_{Solution.query.count()}"

    tests_queue.put((raw_task, raw_code, solution_id, int(cores), user_id, task.id))


def testing_daemon(q):
    while True:
        (raw_task, raw_code, solution_id, max_processes, user_id, task_id) = q.get()
        tester = Tester(raw_task, raw_code, solution_id, max_processes)
        report = tester.start_testing()
        commit_solution(report, raw_code.code, raw_code.compiler.name, user_id, task_id)


def create_code():
    code = [random.choice(string.ascii_uppercase) for i in range(12)]
    code.insert(8, '-')
    code.insert(4, '-')
    return "".join(code)


def send_message(chat_id, message):
    if is_testing:
        global testing_last_message
        testing_last_message = message
        return
    method = "sendMessage"
    token = telegram_token
    url = f"{telegram_url}/bot{token}/{method}"
    data = {"chat_id": chat_id, "text": message, "reply_markup": json.dumps({"remove_keyboard": True})}
    responce = requests.post(url, data=data)
    print(responce.json())


def download_file(file_id):
    if is_testing:
        return testing_downloding_file
    method = "getFile"
    token = telegram_token
    url = f"{telegram_url}/bot{token}/{method}"
    data = {"file_id": file_id}
    responce = requests.post(url, data=data, allow_redirects=True)
    file_path = responce.json()["result"]["file_path"]
    url = f"{telegram_url}/file/bot{token}/{file_path}"
    responce = requests.get(url, allow_redirects=True)
    if not os.path.exists("downloads"):
        mkdir("downloads")

    file = open(f"downloads/{file_id}", "wb")
    file.write(responce.content)
    return file.name


daemon = Process(target=testing_daemon, args=(tests_queue, ))
daemon.start()

data = {"url": url}
url = f"{telegram_url}/bot{telegram_token}/setWebHook"

requests.post(url, data)

flask_app.run(host="0.0.0.0")
