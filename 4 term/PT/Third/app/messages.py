import re

greeting_message = \
    "Добро пожаловать в Fedjaz Contest Bot!\n"\
    "Введите /help чтобы узнать список доступных команд и получить помощь по взаимодействию с ботом"

help_message = \
    "Доступные команды:\n"\
    "/tasks - вывести список задач в соревновании\n"\
    "/join XXXX-XXXX-XXXX - присоединиться к соревнованию по коду\n"\
    "/contests - вывести список соревнований, в которых вы участвуете\n"\
    "/mycontests - вывести список соревнований, созданных вами\n"\
    "/contestinfo - вывести информацию о текущем соревновании\n"\
    "/select НОМЕР - выбрать номер задачи после ввода команды /tasks\n"\
    "/submit - отправить решение. Вы можете отправить решение в виде текста или файла\n"\
    "/compiler ИМЯ - выбрать компилятор для решения\n"\
    "Доступные компиляторы:\n"\
    "Python 3.8 - py\n"\
    "GNU c++ 17 - cpp\n"\
    "/cancel - отменить последнюю команду\n"\
    "/createcontest ИМЯ - создать соревнование\n"\
    "/createtask - создать задачу в соревновании\n"\
    "/standings - вывести статистику по пользователям в соревновании\n"\
    "/userstats ID - вывести статистику по пользователю в соревновании\n"\
    "/deletetask - удалить выбранную задачу из соревнования, если вы являеетесь администратором\n"\
    "/deletecontest - удалить текущее соревнование, если вы являетесь администратором\n"\

command_error_message = "Команда не распознана, либо вы ее используете неправильно"

createtask_message = \
    "Чтобы отправить задачу, вам нужно отправить ее в формате .zip в следующем формате:\n"\
    "1) Файл в json-формате settings.json, содержащий следующие поля:\n"\
    "name - имя задачи\n"\
    "statement - условие задачи\n"\
    "time_limit - ограничение по времени(в милисекундах)\n"\
    "memory_limit - ограничение по памяти(в мегабайтах), максимум 1024 МБ\n"\
    "2) Папка tests - тесты к задаче, парные файлы формата номер_теста, номер_теста.а(максимум 50 тестов)\n"

cancel_error_message = "Нет необходимости отменять предыдущую команду"

cancel_message = "Команда {0} отменена"

task_file_error_message = "Пришлите файл"

task_not_admin_error_message = "Вы не можете добавлять задачи в это соревнование"

task_file_format_error_message = "Формат файла не соответствует заданному"

task_tl_error_message = "Неправильное органичение времени выполнения"

task_ml_error_message = "Неправильное органичение памяти"

task_tests_too_many_error_message = "Слишком много тестов"

task_no_tests_error_message = "Тесты не найдены"

task_success_message = "Задача {0} успешно создана"

task_name_occupied_error_message = "Задача с таким именем уже существует в этом соревновании"

tasks_empty_message = "В этом соревновании нет задач"

tasks_ok_message = "🟢"

tasks_not_ok_message = "🔴"

select_wrong_index_error_message = "Задачи с таким номером нет"

submit_message = "Отправьте решение сообщением в виде текста или в виде файла"

submit_no_task_selected_error_message = "Вы не выбрали задачу"

submit_file_error_message = "Файл не может быть прочитан"

submit_wait_message = "Решение принято на проверку"

compiler_error_message = "Компилятор {0} не найден"

compiler_set_message = "Компилятор {0} установлен"

standings_message = "Статистика по соревнованию {0}\n{1}"

userstats_no_user_error_message = "Пользователь не участвует в этом соревновании"

userstats_message = "Статистика пользователя {0} в соревновании {1}\n{2}"

contests_message = "Список соревнований, в которых вы участвуете:\n{0}"

contests_info_message = "Информация о текущем соревновании:\n" \
                       "Название: {0}\n" \
                       "Пригласительный код: {1}\n" \
                       "Количество пользователей: {2}\n" \
                       "Количество задач: {3}\n" \
                       "Администратор: {4}\n" \
                       "Список пользователей:\n{5}"

createcontest_message = "Соревнование {0} успешно создано, пригласительный ключ - {1}"

joincontest_no_contest_error_message = "Соревнование не найдено"

joincontest_message = "Вы присоединись к соревнованию {0}"

mycontests_message = "Список соревнований, которые вы создали:\n{0}"

mycontests_no_contests_error_message = "Вы пока не создали ни одного соревнования"

deletetask_message = "Задача {0} удалена"

deletetask_not_admin_error_message = "Вы не можете удалить эту задачу, потому что не являетесь создателем соревнования"

deletecontest_message = "Соревнование {0} удалено"

deletecontest_not_admin_error_message = "Вы не можете удалить это соревнование, потому что не являетесь его создателем"

deletecontest_default_contest_error_message = "Вы не можете удалить стандартное соревнование"

admin_allcontests_message = "Список всех соревнований:\n{0}"

command_help_re = r"^/help$"
command_join_re = r"^/join \w{4}-\w{4}-\w{4}$"
command_tasks = r"/tasks"
command_tasks_re = r"^/tasks$"
command_contests_re = r"^/contests$"
command_mycontests_re = r"^/mycontests$"
command_contestinfo_re = r"^/contestinfo$"
command_select_re = r"^/select \d+$"
command_submit = "/submit"
command_submit_re = r"^/submit$"
command_compiler_re = r"^/compiler \w+$"
command_cancel_re = r"^/cancel$"
command_createcontest_re = r"^/createcontest \w+$"
command_createtask_re = r"^/createtask$"
command_createtask= "/createtask"
command_standings_re = r"^/standings$"
command_userstats_re = r"^/userstats \d+$"
command_deletetask_re = r"^/deletetask$"
command_deletecontest_re = r"^/deletecontest$"
admin_command_allcontests_re = r"^/allcontests$"
