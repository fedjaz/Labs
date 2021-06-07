from tests import *
from app import main
from app import messages
from app import db
from app.models import User, Contest, UsersContestsRelation, Solution


def dispose_user(id):
    user = User.query.get(id)
    if user is None:
        return
    contests = Contest.query.filter(Contest.admin_id == id).all()
    if contests is not None:
        for contest in contests:
            main.delete_contest(user, contest)
    relations = UsersContestsRelation.query.filter(UsersContestsRelation.user_id == id).all()
    for relation in relations:
        db.session.delete(relation)
        db.session.commit()
    db.session.delete(user)
    db.session.commit()


def test_registration():
    dispose_user(228)
    main.is_testing = True
    main.proceed_message(228, "Tester", "/start", "")
    assert User.query.get(228) is not None
    assert messages.greeting_message == main.testing_last_message


def test_createcontest():
    main.proceed_message(228, "Tester", "/createcontest testcontest", "")
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    assert contest is not None
    name = contest.name
    key = contest.join_key
    assert messages.createcontest_message.format(name, key) == main.testing_last_message


def test_help():
    main.proceed_message(228, "Tester", "/help", "")
    assert messages.help_message == main.testing_last_message


def test_contest_join():
    contest = Contest.query.filter(Contest.name == "testcontest").first()
    name = contest.name
    key = contest.join_key
    main.proceed_message(228, "Tester", f"/join {key}", "")
    assert messages.joincontest_message.format(name, key) == main.testing_last_message
    main.proceed_message(228, "Tester", "/join BBBB-BBBB-BBBB", "")
    assert messages.joincontest_no_contest_error_message == main.testing_last_message


def test_tasks_empty():
    main.proceed_message(228, "Tester", "/tasks", "")
    assert messages.tasks_empty_message == main.testing_last_message


def test_contestinfo_empty():
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    message = contestinfo_empty.format(contest.join_key)
    main.proceed_message(228, "Tester", "/contestinfo", "")
    assert main.testing_last_message == message


def test_createtask():
    path = os.path.dirname(os.path.realpath(__file__))
    damaged = f"{path}/test_files/task_damaged.zip"

    main.testing_downloding_file = damaged
    main.proceed_message(228, "Tester", "/createtask", "")
    assert messages.createtask_message == main.testing_last_message

    main.proceed_message(228, "Tester", "No task for you!", "")
    assert messages.task_file_error_message == main.testing_last_message

    main.proceed_message(228, "Tester", "", "228")
    assert messages.task_file_format_error_message == main.testing_last_message

    no_tests = f"{path}/test_files/task_no_tests.zip"
    main.testing_downloding_file = no_tests
    main.proceed_message(228, "Tester", "", "228")
    assert messages.task_no_tests_error_message == main.testing_last_message

    wrong_memory = f"{path}/test_files/task_wrong_memory.zip"
    main.testing_downloding_file = wrong_memory
    main.proceed_message(228, "Tester", "", "228")
    assert messages.task_ml_error_message == main.testing_last_message

    wrong_time = f"{path}/test_files/task_wrong_time.zip"
    main.testing_downloding_file = wrong_time
    main.proceed_message(228, "Tester", "", "228")
    assert messages.task_tl_error_message == main.testing_last_message

    too_many = f"{path}/test_files/task_too_many_tests.zip"
    main.testing_downloding_file = too_many
    main.proceed_message(228, "Tester", "", "228")
    assert messages.task_tests_too_many_error_message == main.testing_last_message

    correct = f"{path}/test_files/task_correct.zip"
    main.testing_downloding_file = correct
    main.proceed_message(228, "Tester", "", "228")
    assert messages.task_success_message.format("Квадратный корень") == main.testing_last_message

    contest = Contest.query.filter(Contest.admin_id == 228).first()
    assert contest.tasks[0].name == "Квадратный корень"


def test_tasks():
    main.proceed_message(228, "Tester", "/tasks", "")
    assert "1) Квадратный корень\n" == main.testing_last_message


def test_select_wrong():
    main.proceed_message(228, "Tester", "/select 228", "")
    assert messages.select_wrong_index_error_message == main.testing_last_message


def test_select_correct():
    main.proceed_message(228, "Tester", "/select 1", "")
    assert task_statement == main.testing_last_message


def test_submit_cpp():
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    task = contest.tasks[0]
    path = os.path.dirname(os.path.realpath(__file__))
    main.proceed_message(228, "Tester", "/submit", "")
    assert messages.submit_message == main.testing_last_message

    solution_CE = f"{path}/test_files/solution_CE.cpp"
    main.testing_downloding_file = solution_CE
    main.proceed_message(228, "Tester", "", "228")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "CE"

    main.proceed_message(228, "Tester", "/submit", "")
    solution_RE = f"{path}/test_files/solution_RE.cpp"
    main.testing_downloding_file = solution_RE
    main.proceed_message(228, "Tester", "", "228")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "RE"

    main.proceed_message(228, "Tester", "/submit", "")
    solution_Tl = f"{path}/test_files/solution_TL.cpp"
    main.testing_downloding_file = solution_Tl
    main.proceed_message(228, "Tester", "", "228")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "TL"

    main.proceed_message(228, "Tester", "/submit", "")
    solution_ML = f"{path}/test_files/solution_ML.cpp"
    main.testing_downloding_file = solution_ML
    main.proceed_message(228, "Tester", "", "228")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "ML"

    main.proceed_message(228, "Tester", "/submit", "")
    solution_WA = f"{path}/test_files/solution_WA.cpp"
    main.testing_downloding_file = solution_WA
    main.proceed_message(228, "Tester", "", "228")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "WA"

    main.proceed_message(228, "Tester", "/submit", "")
    solution_OK = f"{path}/test_files/solution_OK.cpp"
    main.testing_downloding_file = solution_OK
    main.proceed_message(228, "Tester", "", "228")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "OK"

    main.proceed_message(228, "Tester", "/submit", "")
    solution_OK = f"{path}/test_files/solution_OK.cpp"
    code = open(solution_OK, "r").read()
    main.proceed_message(228, "Tester", code, "")
    while main.testing_is_running:
        time.sleep(0.1)
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "OK"


def test_cancel():
    main.proceed_message(228, "Tester", "/submit", "")
    main.proceed_message(228, "Tester", "/cancel", "")
    assert messages.cancel_message.format("/submit") == main.testing_last_message
    main.proceed_message(228, "Tester", "/cancel", "")
    assert messages.cancel_error_message == main.testing_last_message


def test_compiler():
    user = User.query.get(228)
    main.proceed_message(228, "Tester", "/compiler cpp", "")
    assert messages.compiler_set_message.format("cpp") == main.testing_last_message
    assert user.selected_compiler == "cpp"
    main.proceed_message(228, "Tester", "/compiler gcc", "")
    assert messages.compiler_error_message.format("gcc") == main.testing_last_message
    assert user.selected_compiler == "cpp"
    main.proceed_message(228, "Tester", "/compiler py", "")
    assert messages.compiler_set_message.format("py") == main.testing_last_message
    assert user.selected_compiler == "py"


def test_tasks_ok():
    main.proceed_message(228, "Tester", "/tasks", "")
    assert "1) Квадратный корень🟢\n" == main.testing_last_message


def test_standings():
    main.proceed_message(228, "Tester", "/standings", "")
    assert standings == main.testing_last_message


def test_userstats():
    main.proceed_message(228, "Tester", "/userstats 227", "")
    assert messages.userstats_no_user_error_message == main.testing_last_message
    main.proceed_message(228, "Tester", "/userstats 228", "")
    assert userstats == main.testing_last_message


def test_submit_py():
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    task = contest.tasks[0]
    path = os.path.dirname(os.path.realpath(__file__))

    main.proceed_message(228, "Tester", "/select 1", "")
    main.proceed_message(228, "Tester", "/submit", "")

    solution_WA = f"{path}/test_files/solution_WA.py"
    main.testing_downloding_file = solution_WA
    main.proceed_message(228, "Tester", "", "228")
    solution = Solution.query.filter(Solution.task_id == task.id,
                                     Solution.user_id == 228,
                                     Solution.is_latest).first()
    assert solution.full_report == main.testing_last_message
    assert solution.test_result == "WA"


def test_tasks_wa():
    main.proceed_message(228, "Tester", "/tasks", "")
    assert "1) Квадратный корень🔴\n" == main.testing_last_message


def test_deletetask():
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    task = contest.tasks[0]
    name = task.name

    main.proceed_message(228, "Tester", "/select 1", "")
    main.proceed_message(228, "Tester", "/deletetask", "")

    assert messages.deletetask_message.format(name) == main.testing_last_message
    assert not contest.tasks


def test_mycontests():
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    main.proceed_message(228, "Tester", "/mycontests", "")
    assert mycontests.format(contest.join_key) == main.testing_last_message


def test_deletecontest():
    contest = Contest.query.filter(Contest.admin_id == 228).first()
    name = contest.name
    main.proceed_message(228, "Tester", "/deletecontest", "")
    assert messages.deletecontest_message.format(name) == main.testing_last_message
    assert Contest.query.filter(Contest.admin_id == 228).first() is None


def test_mycontests_no_contests():
    main.proceed_message(228, "Tester", "/mycontests", "")
    assert messages.mycontests_no_contests_error_message == main.testing_last_message


def test_not_admin():
    main.proceed_message(228, "Tester", "/join AAAA-AAAA-AAAA", "")
    main.proceed_message(228, "Tester", "/select 1", "")
    main.proceed_message(228, "Tester", "/deletetask", "")
    assert messages.deletetask_not_admin_error_message == main.testing_last_message

    main.proceed_message(228, "Tester", "/deletecontest", "")
    assert messages.deletecontest_not_admin_error_message == main.testing_last_message

    main.proceed_message(228, "Tester", "/allcontests", "")
    assert messages.command_error_message == main.testing_last_message


def test_wrong_command():
    main.proceed_message(228, "Tester", "/fakecommand", "")
    assert messages.command_error_message == main.testing_last_message
    dispose_user(228)


if __name__ == "__main__":
    dispose_user(228)
    main.is_testing = True
    test_registration()
    test_createcontest()
    test_createtask()
    test_select_correct()
    test_submit_cpp()


contestinfo_empty = "Информация о текущем соревновании:\n"\
    "Название: testcontest\n"\
    "Пригласительный код: {0}\n"\
    "Количество пользователей: 1\n"\
    "Количество задач: 0\n"\
    "Администратор: @Tester(228)\n"\
    "Список пользователей:\n"\
    "1) @Tester(228)\n"

task_statement = "Задача Квадратный корень\n"\
    "Дано натуральное число n, находящееся под квадратным корнем. Вынести из-под корня максимально возможное число." \
    " Вывести 2 числа — число перед корнем и число под корнем.\n"\
    "Ограничение по времени: 0.5 с\n"\
    "Ограничение по памяти: 128МВ\n"\
    "Ввод: стандартный ввод или input.txt\n"\
    "Вывод: стандартный вывод или output.txt"

mycontests = "Список соревнований, которые вы создали:\n"\
    "testcontest - {0}\n"

standings = "Статистика по соревнованию testcontest\n"\
    "@Tester(228) - 1/1\n"

userstats = "Статистика пользователя @Tester(228) в соревновании testcontest\n"\
    "1) Квадратный корень🟢\n"
