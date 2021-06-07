FROM python:3.8

RUN mkdir -p /home/src/contest

ADD ./requirements.txt /home/src/contest
RUN pip install -r /home/src/contest/requirements.txt

ADD tester_lib /home/src/contest/tester_lib
ADD app /home/src/contest/app
ADD ./main.py /home/src/contest


CMD ["python", "/home/src/contest/main.py"]