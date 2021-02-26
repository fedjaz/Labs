FROM continuumio/miniconda

RUN mkdir -p /home/src/app
WORKDIR /home/src/app

RUN apt update
RUN apt-get install python3-pyqt5 -y

COPY requirements.txt /home/src/app

RUN conda install --file requirements.txt

COPY main.py /home/src/app

CMD ["python", "main.py"]