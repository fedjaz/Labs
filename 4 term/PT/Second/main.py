from fedjaz_serializer.parser.parsers.parserFactory import ParserFactory



d = {"a": "qwe", "b": 123, "c": 456.789}
l = [1, "qwe", 3, 22.8, d, (1, 2, 3, d), False]
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


parser = ParserFactory.createParser("YAML")
file = open("output.txt", "w")
parser.dump(fact, file)
file = open("output.txt", "r")
res = parser.load(file)
print(res(5))
