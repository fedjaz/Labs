from fedjaz_serializer.serializer.serializer import Serializer
from fedjaz_serializer.parser.parsers.toml.stuff import serialize_toml, deserialize_toml


class TomlParser:

    @staticmethod
    def dumps(obj) -> str:
        obj = Serializer.serialize(obj)
        return f"tuple = {serialize_toml(obj)}"

    @staticmethod
    def dump(obj, file):
        file.write(TomlParser.dumps(obj))

    @staticmethod
    def loads(obj: str):
        obj = obj.split('=', 1)[1]

        obj = deserialize_toml(obj.replace("\\n", "\n").strip())
        return Serializer.deserialize(obj)

    @staticmethod
    def load(file):
        return TomlParser.loads(file.read())
