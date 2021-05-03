from fedjaz_serializer.serializer.serializer import Serializer


s = Serializer()

d = {"a": "qwe", "b": 123, "c": 456.789}
l = [1, "qwe", 3, 22.8, d, (1, 2, 3, d), False]

ser = s.serialize(l)
res = s.deserialize(ser)
print(l)
print(res)
print(l == res)
