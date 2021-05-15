from fedjaz_serializer.parser.parsers.parserFactory import ParserFactory

p = ParserFactory.createParser("yaml")
file = open("output.yml", "r")
res = p.load(file)
p = ParserFactory.createParser("json")
file.close()
file = open("output.json", "w")
p.dump(res, file)
