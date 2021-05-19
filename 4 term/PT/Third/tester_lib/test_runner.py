import os
import time
from os import mkdir
from tester_lib.models.test_report import TestReport, TestResults

import docker.models.images


class TestRunner:

    def __init__(self, client: docker.client.DockerClient, image: docker.models.images.Image, task, solution_id):
        self.solution_id = solution_id
        self.task = task
        self.image = image
        self.client = client

    def run_test(self, test_index):
        test = self.task.tests[test_index]
        mkdir(f"{self.solution_id}/IO/{test_index}")
        io_global_path = f"{os.getcwd()}/{self.solution_id}/IO/{test_index}"
        input_global_path = f"{io_global_path}/input.txt"
        output_global_path = f"{io_global_path}/output.txt"
        input_file = open(input_global_path, "w")
        output_file = open(output_global_path, "w")
        input_file.write(test.input_string)
        input_file.close()
        output_file.close()

        self.client.containers.prune()
        container = \
            self.client.containers.run(self.image.id,
                                       detach=True,
                                       mem_limit=f"{self.task.memory_limit}M",
                                       name=f"{self.solution_id}_{test_index}",
                                       volumes={input_global_path: {"bind": "/home/src/app/input.txt", "mode": "ro"},
                                                output_global_path: {"bind": "/home/src/app/output.txt", "mode": "rw"},
                                                },
                                       network_disabled=True
                                       )

        start_time = time.time()
        report = TestReport(None, None, test_index, 256)
        while time.time() - start_time < 1:
            container.reload()
            if container.status != "exited":
                time.sleep(0.025)
            else:
                total_time = time.time() - start_time
                report.time = total_time
                self.client.containers.prune()
                output_file = open(f"{self.solution_id}/IO/{test_index}/output.txt", "r")
                result = output_file.read()
                output_file.close()

                if result == test.output_string:
                    report.result = TestResults.OK
                elif container.attrs["State"]["OOMKilled"]:
                    report.result = TestResults.ML
                elif container.attrs["State"]["ExitCode"] != 0:
                    report.result = TestResults.RE
                else:
                    report.result = TestResults.WA

                return report

        container.reload()
        if container.status != "exited":
            container.kill()
        report.time = time.time() - start_time
        report.result = TestResults.TL
        return report
