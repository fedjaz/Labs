import subprocess
import time

from psutil import virtual_memory
from json import load, dump

file = open("settings.json", "r")
settings = load(file)

compiler = settings["compiler"]
time_limit = int(settings["time_limit"])
memory_limit = int(settings["memory_limit"])
start_mem = virtual_memory().used

input_file = open("input.txt", "r")
output_file = open("output.txt", "w")

if compiler == "py":
    process = subprocess.Popen(["python3", "solution.py"], stdin=input_file, stdout=output_file, cwd="/home/src/app")
else:
    process = subprocess.Popen("./executable.cpp", stdin=input_file, stdout=output_file, cwd="/home/src/app")

start_time = time.time()

max_memory = 1024 ** 2
result = {"result": "OK"}
while time.time() - start_time < time_limit / 1000:
    exit_code = process.poll()
    cur_time = time.time() - start_time
    cur_memory = virtual_memory().used - start_mem
    max_memory = max(cur_memory, max_memory)
    if exit_code is None:
        if cur_memory > memory_limit * (1024 ** 2):
            result = {"result": "ML", "time": cur_time, "memory": cur_memory}
            process.terminate()
            break
    else:
        result = {"time": cur_time, "memory": round(max_memory)}
        if exit_code != 0:
            result["result"] = "RE"
        else:
            result["result"] = "OK"
        break

if process.poll() is None and result["result"] != "ML":
    result = {"result": "TL", "time": time.time() - start_time, "memory": max_memory}
    process.terminate()


file = open("result.json", "w")
dump(result, file)
exit(0)

