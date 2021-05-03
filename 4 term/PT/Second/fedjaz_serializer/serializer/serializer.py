import inspect
import re
from pydoc import locate

class Serializer:

    def serialize(self, obj):
        ans = {}
        object_type = type(obj)
        if object_type == list:
            ans["type"] = "list"
            ans["value"] = [self.serialize(i) for i in obj]
        elif object_type == dict:
            ans["type"] = "dict"
            ans["value"] = {}

            for i in obj:
                key = self.serialize(i)
                key = tuple((k, key[k]) for k in key)
                value = self.serialize(obj[i])
                ans["value"][key] = value
        elif object_type == tuple:
            ans["type"] = "tuple"
            ans["value"] = tuple([self.serialize(i) for i in obj])
        elif inspect.isroutine(obj):
            exit(0)
        else:
            typestr = re.search(r"\'(\w+)\'", str(object_type)).group(1)
            ans["type"] = typestr
            ans["value"] = obj

        return ans


    def deserialize(self, d:dict):
        object_type = d["type"]
        ans = None
        if object_type == "list":
            ans = [self.deserialize(i) for i in d["value"]]
        elif object_type == "dict":
            ans = {}
            for i in d["value"]:
                key = dict((a, b) for a, b in i)
                val = self.deserialize(d["value"][i])
                ans[self.deserialize(key)] = val
        elif object_type == "tuple":
            ans = tuple([self.deserialize(i) for i in d["value"]])
        elif object_type == "function":
            exit(0)
        else:
            ans = locate(object_type)(d["value"])

        return ans

