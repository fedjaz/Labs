def serialize_yaml(obj) -> str:
    if type(obj) == tuple:
        ans = "!!python/tuple"
        parsed = []
        if len(obj) == 0:
            return f"{ans} []"
        for i in obj:
            parsed.append(serialize_yaml(i).replace("\n", "\n  "))
        parsed.insert(0, ans)
        return "\n- ".join(parsed)
    else:
        if type(obj) == str and '\n' in obj:
            return f"\"{obj}\""
        else:
            return str(obj)




def deserialize_yaml(obj: str):
    splitted = obj.split("\n", 1)
    if splitted[0] == "!!python/tuple":
        splitted = splitted[1].split("\n")

        substr = ""
        parsed = []
        quote = False
        for i in splitted:
            istr = i[2:]
            spacecount = len(istr) - len(istr.lstrip(' '))
            if istr[0] == '\"':
                quote = True

            if spacecount == 0:
                if substr == "":
                    substr += istr
                else:
                    parsed.append(deserialize_yaml(substr))


    elif splitted[0] == "!!python/tuple []":
        return tuple()
    else:
        return str(obj[2:])
