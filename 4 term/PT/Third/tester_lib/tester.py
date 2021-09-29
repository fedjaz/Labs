import multiprocessing
from json import dump
from os import mkdir
import os
import tester_lib
from shutil import copyfile, rmtree
from tester_lib.test_runner import TestRunner
from tester_lib.models.test_report import TestResults
from tester_lib.models.task_report import TaskReport
from tester_lib.models.code import Code, Compiler
from tester_lib.config.gcc_config import compile_string
from tester_lib.config.docker_config import image_rm
import docker
import subprocess
from multiprocessing import Process
from multiprocessing.queues import Queue
from tester_lib.process_pool import TestProcessPool


class Tester:
    def __init__(self, task, code, solution_id, max_processes=4):
        self.task = task
        self.code = code
        self.solution_id = solution_id
        self.max_processes = max_processes
        self.docker_client = docker.from_env()

    def start_testing(self):
        task_report = TaskReport([], 0, len(self.task.tests), TestResults.OK, "")
        image = self.build_docker_image()

        cur_path = "/home/src/contest/tester"
        if type(image) == bytes:
            task_report.total_result = TestResults.CE
            task_report.message = image.decode("ISO-8859-1")
            #rmtree(f"{cur_path}/{self.solution_id}")
            return task_report

        lock = multiprocessing.Lock()
        test_runner = TestRunner(self.docker_client, image[0], self.task, self.solution_id, lock)

        mkdir(f"{cur_path}/{self.solution_id}")
        mkdir(f"{cur_path}/{self.solution_id}/IO/")
        self.docker_client.containers.prune()
        indexes = range(0, len(self.task.tests))

        pool = TestProcessPool(self.max_processes)
        res = pool.map(test_runner.run_test, indexes)
        for i in res:
            task_report.test_reports.append(i)

            if i.result != TestResults.OK:
                task_report.total_result = i.result
                break
            else:
                task_report.passed += 1

        self.docker_client.containers.prune()
        self.docker_client.images.prune()
        rmtree(f"{cur_path}/{self.solution_id}")
        self.remove_image(self.solution_id)
        return task_report

    def build_docker_image(self):
        cur_path = tester_lib.__path__[0]
        if os.path.exists(f"{cur_path}/{self.solution_id}"):
            rmtree(f"{cur_path}/{self.solution_id}")

        mkdir(f"{cur_path}/{self.solution_id}")
        if self.code.compiler == Compiler.py:
            dockerfile_name = "DockerfilePY"
        else:
            dockerfile_name = "DockerfileCPP"

        copyfile(f"{cur_path}/config/{dockerfile_name}", f"{cur_path}/{self.solution_id}/Dockerfile")
        copyfile(f"{cur_path}/config/runner.py", f"{cur_path}/{self.solution_id}/runner.py")
        settings_file = open(f"{cur_path}/{self.solution_id}/settings.json", "w")
        settings = {"compiler": str(self.code.compiler.name),
                    "time_limit": str(self.task.time_limit),
                    "memory_limit": str(self.task.memory_limit)}
        dump(settings, settings_file)
        settings_file.close()
        if self.code.compiler == Compiler.py:
            solution_file = open(f"{cur_path}/{self.solution_id}/solution.py", "w")
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

    def remove_image(self, name):
        self.docker_client.images.remove(name)
