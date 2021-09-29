from fedjaz_serializer.serializer.serializer import Serializer
from fedjaz_serializer.parser.parsers.json.stuff import serialize_json, deserialize_json


class JsonParser:

    @staticmethod
    def dumps(obj) -> str:
        obj = Serializer.serialize(obj)
        return serialize_json(obj).replace("\n", "\\n")

    @staticmethod
    def dump(obj, file):
        file.write(JsonParser.dumps(obj))

    @staticmethod
    def loads(obj: str):
        obj = deserialize_json(obj.replace("\\n", "\n"))
        return Serializer.deserialize(obj)

    @staticmethod
    def load(file):
        return JsonParser.loads(file.read())
