import matplotlib
matplotlib.use('agg')
import numpy as np
from matplotlib import pyplot as pp
import json


jsonReader = json.load(open("/resources/input.txt"))
x = [float(i) for i in (jsonReader["x"])]
y = [float(i) for i in (jsonReader["y"])]
size = int(jsonReader["size"])

if bool(jsonReader["noise"]):
    noiseSize = float(jsonReader["noiseSize"])
    x += np.random.normal(0, noiseSize, len(x))
    y += np.random.normal(0, noiseSize, len(x))

model = np.poly1d(np.polyfit(x, y, size))
line = np.linspace(min(x), max(x), len(x) * 10)

pp.plot(line, model(line))
pp.scatter(x, y)

pp.savefig("/resources/output.png")
