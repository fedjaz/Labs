from tester_lib.tester import Tester
from tester_lib.models.task import Task
from tester_lib.models.test import Test


tests = []
for i in range(1, 34):
    input = open(f"joint_sets/{str(i).zfill(3)}", "r")
    output = open(f"joint_sets/{str(i).zfill(3)}.a", "r")
    test = Test(input.read(), output.read())
    tests.append(test)

task = Task(tests, 1000, 256)
solution = open("solution.py", "r")

tester = Tester(task, solution.read(), "solution_2")
report = tester.start_testing()
for i in report.test_reports:
    print(f"{i.index + 1}){i.result.name} - {i.time:.2f}s")
print(f"{report.total_result.name}({report.passed}/{report.total})")
