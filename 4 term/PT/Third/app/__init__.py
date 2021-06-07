from flask import Flask
from app.config import Config
from flask_sqlalchemy import SQLAlchemy
from flask_migrate import Migrate

from app.telegram_config import admin_id

flask_app = Flask(__name__)
flask_app.config.from_object(Config)
db = SQLAlchemy(flask_app)
migrate = Migrate(flask_app, db)

from app import models
from app.models import *


default_contest = Contest.query.filter(Contest.join_key == "AAAA-AAAA-AAAA").first()
if default_contest is None:
    admin_user = User(id=admin_id)
    db.session.add(admin_user)
    default_contest = Contest(admin_id=admin_id,
                              id=1,
                              name="Fedjaz Contest",
                              join_key="AAAA-AAAA-AAAA")
    db.session.add(default_contest)
    db.session.commit()

