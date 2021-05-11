import inspect
import re
from fedjaz_serializer.serializer.stuff import CODE_OBJECT_ARGS, FUNCTION_ATTRIBUTES
from pydoc import locate
from types import CodeType, FunctionType


class Serializer:



    @staticmethod
    def serialize(obj):
        ans = {}
        object_type = type(obj)
        if object_type == list:
            ans["type"] = "list"
            ans["value"] = tuple([Serializer.serialize(i) for i in obj])
        elif object_type == dict:
            ans["type"] = "dict"
            ans["value"] = {}

            for i in obj:
                key = Serializer.serialize(i)
                value = Serializer.serialize(obj[i])
                ans["value"][key] = value
            ans["value"] = tuple((k, ans["value"][k]) for k in ans["value"])
        elif object_type == tuple:
            ans["type"] = "tuple"
            ans["value"] = tuple([Serializer.serialize(i) for i in obj])
        elif object_type == bytes:
            ans["type"] = "bytes"
            ans["value"] = tuple([Serializer.serialize(i) for i in obj])
        elif obj is None:
            ans["type"] = "NoneType"
            ans["value"] = None
        elif inspect.isroutine(obj):
            ans["type"] = "function"
            ans["value"] = {}
            members = inspect.getmembers(obj)
            members = [i for i in members if i[0] in FUNCTION_ATTRIBUTES]
            for i in members:
                key = Serializer.serialize(i[0])
                value = Serializer.serialize(i[1])
                ans["value"][key] = value
                if i[0] == "__code__":
                    key = Serializer.serialize("__globals__")
                    ans["value"][key] = {}
                    names = i[1].__getattribute__("co_names")
                    glob = obj.__getattribute__("__globals__")
                    globdict = {}
                    for name in names:
                        if name == obj.__name__:
                            globdict[name] = obj.__name__
                        elif name in glob and not inspect.ismodule(name) and name not in __builtins__:
                            globdict[name] = glob[name]
                    ans["value"][key] = Serializer.serialize(globdict)
            ans["value"] = tuple((k, ans["value"][k]) for k in ans["value"])

        elif isinstance(obj, (int, float, complex, bool, str)):
            typestr = re.search(r"\'(\w+)\'", str(object_type)).group(1)
            ans["type"] = typestr
            ans["value"] = obj
        else:
            ans["type"] = "instance"
            ans["value"] = {}
            members = inspect.getmembers(obj)
            members = [i for i in members if not callable(i[1])]
            for i in members:
                key = Serializer.serialize(i[0])
                val = Serializer.serialize(i[1])
                ans["value"][key] = val
            ans["value"] = tuple((k, ans["value"][k]) for k in ans["value"])

        ans = tuple((k, ans[k]) for k in ans)
        return ans

    @staticmethod
    def deserialize(d):
        d = dict((a, b) for a, b in d)
        object_type = d["type"]
        ans = None
        if object_type == "list":
            ans = [Serializer.deserialize(i) for i in d["value"]]
        elif object_type == "dict":
            ans = {}
            for i in d["value"]:
                val = Serializer.deserialize(i[1])
                ans[Serializer.deserialize(i[0])] = val
        elif object_type == "tuple":
            ans = tuple([Serializer.deserialize(i) for i in d["value"]])
        elif object_type == "function":
            func = [0] * 4
            code = [0] * 16
            glob = {"__builtins__": __builtins__}
            for i in d["value"]:
                key = Serializer.deserialize(i[0])
                if key != "__code__" and key != "__globals__":
                    index = FUNCTION_ATTRIBUTES.index(key)
                    func[index] = (Serializer.deserialize(i[1]))
                elif key == "__globals__":
                    globdict = Serializer.deserialize(i[1])
                    for globkey in globdict:
                        glob[globkey] = globdict[globkey]
                else:
                    val = i[1][1][1]
                    for arg in val:
                        codeArgKey = Serializer.deserialize(arg[0])
                        if codeArgKey != "__doc__":
                            codeArgVal = Serializer.deserialize(arg[1])
                            index = CODE_OBJECT_ARGS.index(codeArgKey)
                            code[index] = codeArgVal

                    code = CodeType(*code)

            func[0] = code
            func.insert(1, glob)

            ans = FunctionType(*func)
            if ans.__name__ in ans.__getattribute__("__globals__"):
                ans.__getattribute__("__globals__")[ans.__name__] = ans

        elif object_type == "NoneType":
            ans = None
        elif object_type == "bytes":
            ans = bytes([Serializer.deserialize(i) for i in d["value"]])
        else:
            if object_type != "bool":
                ans = locate(object_type)(d["value"])
            elif type(d["value"]) == bool:
                ans = d["value"]
            else:
                ans = d["value"] == "True"

        return ans

