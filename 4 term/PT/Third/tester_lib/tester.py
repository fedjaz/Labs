import time
from os import mkdir
import os
from shutil import copyfile, rmtree
from tester_lib.test_runner import TestRunner
from tester_lib.models.test_report import TestResults
from tester_lib.models.task_report import TaskReport
from tester_lib.models.code import Code, Compiler
from tester_lib.config.gcc_config import compile_string
import docker
import subprocess


class Tester:
    def __init__(self, task, code, solution_id):
        self.task = task
        self.code = code
        self.solution_id = solution_id
        self.docker_client = docker.from_env()

    def start_testing(self):
        task_report = TaskReport([], 0, len(self.task.tests), TestResults.OK, "")
        image = self.build_docker_image()

        if type(image) == bytes:
            task_report.total_result = TestResults.CE
            task_report.message = image.decode("UTF-8")
            return task_report

        test_runner = TestRunner(self.docker_client, image[0], self.task, self.solution_id)

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
        if self.code.compiler == Compiler.py:
            dockerfile_name = "DockerfilePY"
        else:
            dockerfile_name = "DockerfileCPP"

        copyfile(f"{cur_path}/tester_lib/config/{dockerfile_name}", f"{cur_path}/{self.solution_id}/Dockerfile")
        if self.code.compiler == Compiler.py:
            solution_file = open(f"{self.solution_id}/solution.py", "w")
            solution_file.write(self.code.code)
            solution_file.close()
        else:
            err = self.compile_gcc(f"{cur_path}/{self.solution_id}")
            if err != b'':
                return err

        return self.docker_client.images.build(
            path=f"{cur_path}/{self.solution_id}/",
            tag=self.solution_id,
        )

    def compile_gcc(self, path):
        solution_file = open(f"{path}/solution.cpp", "w")
        solution_file.write(self.code.code)
        solution_file.close()

        compile_str = compile_string.format("solution.cpp", "executable.cpp")
        process = subprocess.Popen(compile_str.split(), stdout=subprocess.PIPE, stderr=subprocess.PIPE, cwd=path)
        out, err = process.communicate()
        return err

