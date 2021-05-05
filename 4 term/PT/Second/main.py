from fedjaz_serializer.serializer.serializer import Serializer


s = Serializer()


def f1(n1):
    if n1 == 0:
        return 1
    else:
        return n1 * f1(n1 - 1)


def fact(n):
    return f1(n)


def f(a):
    return a * 2


d = {"a": "qwe", "b": 123, "c": 456.789}
l = [1, "qwe", 3, 22.8, d, (1, 2, 3, d), False]
n = 25


b = bytes([1, 1, 1, 1])

ser = s.serialize(fact)

res = s.deserialize(ser)
print(fact(5))
print(res(5))
#print(f == res)
