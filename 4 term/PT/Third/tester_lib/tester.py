from os import mkdir
import os
from shutil import copyfile, rmtree
from tester_lib.test_runner import TestRunner
from tester_lib.models.test_report import TestReport, TestResults
from tester_lib.models.task_report import TaskReport
import docker


class Tester:
    def __init__(self, task, code, solution_id):
        self.task = task
        self.code = code
        self.solution_id = solution_id
        self.docker_client = docker.from_env()

    def start_testing(self):
        image = self.build_docker_image()
        test_runner = TestRunner(self.docker_client, image[0], self.task, self.solution_id)
        task_report = TaskReport([], 0, len(self.task.tests), TestResults.OK)
        mkdir(f"{self.solution_id}/IO/")
        for i in range(0, len(self.task.tests)):
            report = test_runner.run_test(i)
            task_report.test_reports.append(report)
            if report.result == TestResults.OK:
                task_report.passed += 1
            else:
                task_report.total_result = report.result
                break

        rmtree(self.solution_id)
        return task_report

    def build_docker_image(self):
        if os.path.exists(self.solution_id):
            rmtree(self.solution_id)
        mkdir(self.solution_id)
        cur_path = os.getcwd()
        copyfile(f"{cur_path}/tester_lib/config/Dockerfile", f"{cur_path}/{self.solution_id}/Dockerfile")
        solution_file = open(f"{self.solution_id}/solution.py", "w")
        solution_file.write(self.code)
        solution_file.close()
        return self.docker_client.images.build(
            path=f"{cur_path}/{self.solution_id}/",
            tag=self.solution_id,
        )
