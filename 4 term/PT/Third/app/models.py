from app import db


class Base(db.Model):
    __abstract__ = True
    id = db.Column(db.Integer, primary_key=True, index=True)


class User(Base):
    __tablename__ = "users"
    username = db.Column(db.String(64), index=True)
    last_command = db.Column(db.String(64))
    state = db.Column(db.String(64))
    selected_compiler = db.Column(db.String(10))
    active_contest_id = db.Column(db.Integer)
    solutions = db.relationship("Solution", backref='users', lazy=True)


class Solution(Base):
    __tablename__ = "solutions"
    task_id = db.Column(db.Integer, db.ForeignKey("tasks.id"))
    user_id = db.Column(db.Integer, db.ForeignKey("users.id"))
    is_latest = db.Column(db.Boolean)
    code = db.Column(db.Text)
    compiler = db.Column(db.String(10))
    test_result = db.Column(db.String(2))
    full_report = db.Column(db.Text)

    def __str__(self):
        return f"{self.test_result}{self.full_report}"


class Contest(Base):
    __tablename__ = "contests"
    admin_id = db.Column(db.Integer)
    name = db.Column(db.String(64))
    join_key = db.Column(db.String(14), index=True)
    tasks = db.relationship("Task", backref="contests", lazy=True)


class Task(Base):
    __tablename__ = "tasks"
    name = db.Column(db.String(64))
    statement = db.Column(db.Text)
    time_limit = db.Column(db.Integer)
    memory_limit = db.Column(db.Integer)
    tests = db.relationship("Test", backref='tasks', lazy=True)
    solutions = db.relationship("Solution", backref='tasks', lazy=True)
    contest_id = db.Column(db.Integer, db.ForeignKey("contests.id"))

    def __str__(self):
        return f"Задача {self.name}\n" \
               f"{self.statement}\n" \
               f"Ограничение по времени: {self.time_limit / 1000} с\n" \
               f"Ограничение по памяти: {self.memory_limit}МВ\n" \
               f"Ввод: стандартный ввод или input.txt\n" \
               f"Вывод: стандартный вывод или output.txt"


class Test(Base):
    __tablename__ = "tests"
    index = db.Column(db.Integer)
    input_string = db.Column(db.Text)
    output_string = db.Column(db.Text)
    task_id = db.Column(db.Integer, db.ForeignKey("tasks.id"))


class UsersContestsRelation(db.Model):
    __tablename__ = "users_contests_relations"
    id = db.Column(db.Integer, primary_key=True, index=False)
    user_id = db.Column(db.Integer, db.ForeignKey("users.id"), index=True)
    contest_id = db.Column(db.Integer, db.ForeignKey("contests.id"), index=True)
