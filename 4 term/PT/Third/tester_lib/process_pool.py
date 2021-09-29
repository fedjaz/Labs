import multiprocessing
from multiprocessing import Process
from multiprocessing.queues import Queue
from queue import Queue as SimpleQueue
from tester_lib.models.test_report import TestResults


class TestProcessPool:
    def __init__(self, max_processes):
        self.max_processes = max_processes

    def map(self, target, args):
        processes = SimpleQueue()
        outputs = [None] * len(args)
        for i in args:
            q = Queue(1, ctx=multiprocessing.get_context())
            p = Process(target=target, args=(i, q))
            processes.put((p, q))

        active = []
        is_failed = False
        for i in range(0, min(self.max_processes, len(args))):
            p = processes.get()
            active.append(p)
            p[0].start()

        while active or not processes.empty():
            for i in active:
                if not i[0].is_alive():
                    active.remove(i)
                    res = i[1].get()
                    outputs[res.index] = res
                    if res.result != TestResults.OK:
                        is_failed = True
            while not processes.empty() and len(active) < self.max_processes:
                p = processes.get()
                if not is_failed:
                    p[0].start()
                    active.append(p)

        return outputs
