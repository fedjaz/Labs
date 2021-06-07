import time
from json import load
from os import mkdir
import tester_lib
from tester_lib.models.test_report import TestReport, TestResults
import os

import docker.models.images


class TestRunner:

    def __init__(self, client: docker.client.DockerClient, image: docker.models.images.Image, task, solution_id, lock):
        self.solution_id = solution_id
        self.task = task
        self.image = image
        self.client = client
        self.lock = lock

    def run_test(self, test_index, queue):
        report = TestReport(None, None, test_index, 256)
        real_path = os.getenv("real_path")
        cur_path = "/home/src/contest/tester"
        test = self.task.tests[test_index]
        mkdir(f"{cur_path}/{self.solution_id}/IO/{test_index}")
        io_global_path = f"{cur_path}/{self.solution_id}/IO/{test_index}"
        input_global_path = f"{io_global_path}/input.txt"
        output_global_path = f"{io_global_path}/output.txt"
        result_global_path = f"{io_global_path}/result.json"
        input_file = open(input_global_path, "w")
        output_file = open(output_global_path, "w")
        result_file = open(result_global_path, "w")
        input_file.write(test.input_string)
        input_file.close()
        output_file.close()
        result_file.close()

        io_global_path = f"{real_path}/{self.solution_id}/IO/{test_index}"
        input_global_path = f"{io_global_path}/input.txt"
        output_global_path = f"{io_global_path}/output.txt"
        result_global_path = f"{io_global_path}/result.json"

        container = \
            self.client.containers.run(self.image.id,
                                       mem_limit="2048m",
                                       memswap_limit=0,
                                       name=f"{self.solution_id}_{test_index}",
                                       volumes={input_global_path: {"bind": "/home/src/app/input.txt", "mode": "ro"},
                                                output_global_path: {"bind": "/home/src/app/output.txt", "mode": "rw"},
                                                result_global_path: {"bind": "/home/src/app/result.json", "mode": "rw"},
                                                },
                                       network_disabled=True,
                                       )

        io_global_path = f"{cur_path}/{self.solution_id}/IO/{test_index}"
        input_global_path = f"{io_global_path}/input.txt"
        output_global_path = f"{io_global_path}/output.txt"
        result_global_path = f"{io_global_path}/result.json"
        result_file = open(result_global_path, "r")
        result = load(result_file)
        report.result = TestResults[result["result"]]
        if result["result"] == "OK":
            output_file = open(output_global_path, "r")
            output_string = output_file.read().strip("\n ")
            test_output = test.output_string.strip("\n ")
            if output_string != test_output:
                report.result = TestResults.WA
        else:
            report.result = TestResults[result["result"]]

        report.time = result["time"]
        report.memory = result["memory"] / 1024

        queue.put(report)
        return report
