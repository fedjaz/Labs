import os

telegram_token = os.getenv("telegram_token")
if telegram_token is None:
    telegram_token = "1615904671:AAFbgzvPGoIbGkOziq3pSk8EJn23m4raLO4"
admin_id = os.getenv("admin_id")
if admin_id is None:
    admin_id = 375504575
telegram_url = "https://api.telegram.org"
url = os.getenv("url")
if url is None:
    url = "https://f1965ad2838b.ngrok.io"
cores = os.getenv("cores")
if cores is None:
    cores = 4
