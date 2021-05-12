from fedjaz_serializer.parser.parsers.json.json import JsonParser
from fedjaz_serializer.parser.parsers.yaml.yaml import YamlParser
from fedjaz_serializer.serializer.serializer import Serializer
import yaml

s = Serializer()
p = YamlParser()


d = {"a": "qwe", "b": 123, "c": 456.789}
l = [1, "qwe", 3, 22.8, d, (1, 2, 3, d), False, None]
n = 228

b = bytes([1, 1, 1, 1])


def f1(n1):
    if n1 == 0:
        return 1
    else:
        return n1 * f1(n1 - 1)


def fact(n):
    print(l)
    return f1(n)


file = open("output.yml", "w")
#fileyy = open("outputyy.yml", "w")
#yaml.dump(s.serialize(fact), fileyy)
p.dump(fact, file)
file = open("output.yml", "r")
res = p.load(file)
print(res(5))
