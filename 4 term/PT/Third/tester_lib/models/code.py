from enum import Enum


class Compiler(Enum):
    py = 1,
    cpp = 2,


class Code:
    def __init__(self, code, compiler):
        self.code = code
        self.compiler = compiler
