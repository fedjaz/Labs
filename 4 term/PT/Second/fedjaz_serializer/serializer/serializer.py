import inspect
import re
from pydoc import locate
from types import CodeType, FunctionType


class Serializer:

    FUNCTION_ATTRIBUTES = [
        "__code__",
        "__name__",
        "__defaults__",
        "__closure__"
    ]

    CODE_OBJECT_ARGS = (
        'co_argcount',
        'co_posonlyargcount',
        'co_kwonlyargcount',
        'co_nlocals',
        'co_stacksize',
        'co_flags',
        'co_code',
        'co_consts',
        'co_names',
        'co_varnames',
        'co_filename',
        'co_name',
        'co_firstlineno',
        'co_lnotab',
        'co_freevars',
        'co_cellvars'
    )

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
                #key = tuple((k, key[k]) for k in key)
                value = self.serialize(obj[i])
                ans["value"][key] = value
            ans["value"] = tuple((k, ans["value"][k]) for k in ans["value"])
        elif object_type == tuple:
            ans["type"] = "tuple"
            ans["value"] = tuple([self.serialize(i) for i in obj])
        elif object_type == bytes:
            ans["type"] = "bytes"
            ans["value"] = [self.serialize(i) for i in obj]
        elif obj is None:
            ans["type"] = "NoneType"
            ans["value"] = None
        elif inspect.isroutine(obj):
            ans["type"] = "function"
            ans["value"] = {}
            members = inspect.getmembers(obj)
            members = [i for i in members if i[0] in self.FUNCTION_ATTRIBUTES]
            for i in members:
                key = self.serialize(i[0])
                #key = tuple((k, key[k]) for k in key)
                value = self.serialize(i[1])
                ans["value"][key] = value
                if i[0] == "__code__":
                    key = self.serialize("__globals__")
                    #key = tuple((k, key[k]) for k in key)
                    ans["value"][key] = {}
                    names = i[1].__getattribute__("co_names")
                    glob = obj.__getattribute__("__globals__")
                    globdict = {}
                    for name in names:
                        if name == obj.__name__:
                            globdict[name] = obj.__name__
                        elif name in glob and not inspect.ismodule(name) and name not in __builtins__:
                            globdict[name] = glob[name]
                    ans["value"][key] = self.serialize(globdict)
                    #ans["value"][key] = tuple((k, ans["value"][key][k]) for k in ans["value"][key])
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
                key = self.serialize(i[0])
                #key = tuple((k, key[k]) for k in key)
                val = self.serialize(i[1])
                ans["value"][key] = val
            ans["value"] = tuple((k, ans["value"][k]) for k in ans["value"])

        ans = tuple((k, ans[k]) for k in ans)
        return ans

    def deserialize(self, d):
        d = dict((a, b) for a, b in d)
        object_type = d["type"]
        ans = None
        if object_type == "list":
            ans = [self.deserialize(i) for i in d["value"]]
        elif object_type == "dict":
            ans = {}
            for i in d["value"]:
                #key = dict((a, b) for a, b in i)
                val = self.deserialize(i[1])
                ans[self.deserialize(i[0])] = val
        elif object_type == "tuple":
            ans = tuple([self.deserialize(i) for i in d["value"]])
        elif object_type == "function":
            func = [0] * 4
            code = [0] * 16
            glob = {"__builtins__": __builtins__}
            for i in d["value"]:
                key = self.deserialize(i[0])
                if key != "__code__" and key != "__globals__":
                    index = self.FUNCTION_ATTRIBUTES.index(key)
                    func[index] = (self.deserialize(i[1]))
                elif key == "__globals__":
                    globdict = self.deserialize(i[1])
                    for globkey in globdict:
                        glob[globkey] = globdict[globkey]
                else:
                    val = i[1][1][1]
                    for arg in val:
                        codeArgKey = self.deserialize(arg[0])
                        if codeArgKey != "__doc__":
                            codeArgVal = self.deserialize(arg[1])
                            index = self.CODE_OBJECT_ARGS.index(codeArgKey)
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
            ans = bytes([self.deserialize(i) for i in d["value"]])
        else:
            ans = locate(object_type)(d["value"])

        return ans

