from fedjaz_serializer.parser.parsers.json.json import JsonParser
from fedjaz_serializer.parser.parsers.yaml.yaml import YamlParser
from fedjaz_serializer.parser.parsers.toml.toml import TomlParser


class ParserFactory:

    @staticmethod
    def createParser(name: str):
        name = name.lower()
        if name == "json":
            return JsonParser()
        elif name == "yaml":
            return YamlParser()
        elif name == "toml":
            return TomlParser()
        else:
            raise ValueError
