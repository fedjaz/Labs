from enum import Enum


class TestResults(Enum):
    OK = 1,
    WA = 2,
    TL = 3,
    ML = 4,
    RE = 5,
    CE = 6,


class TestReport:
    def __init__(self, result, time, index, memory):
        self.memory = memory
        self.index = index
        self.time = time
        self.result = result

    def __int__(self, index):
        self.index = index
