from fedjaz_serializer.parser.parsers.parserFactory import ParserFactory


def test_fact(n=5):
    if n == 0:
        return 1
    else:
        return n * test_fact(n - 1)


p = ParserFactory.createParser("yaml")
file = open("output.yml", "w")
p.dump(test_fact, file)
file = open("output.yml", "r")
res = p.load(file)
print(res())
