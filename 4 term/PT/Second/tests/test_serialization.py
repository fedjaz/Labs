from fedjaz_serializer.serializer.serializer import Serializer
from fedjaz_serializer.parser.parsers.parserFactory import ParserFactory
from tests.test_stuff import *
import unittest


def serialize_and_compare_obj(obj, tester):
    s = Serializer()
    ser = s.serialize(obj)
    res = s.deserialize(ser)
    tester.assertEqual(res, obj)


def serialize_and_compare_func(func, tester):
    s = Serializer()
    ser = s.serialize(func)
    res = s.deserialize(ser)
    tester.assertEqual(res(5), func(5))


def parse_and_compare_func(func, format, tester):
    p = ParserFactory.createParser(format)
    format = "yml" if format.lower() == "yaml" else format.lower()
    file = open(f"output.{format}", "w")
    p.dump(func, file)
    file.close()
    file = open(f"output.{format}", "r")
    res = p.load(file)
    file.close()
    tester.assertEqual(res(5), func(5))


class TestClass(unittest.TestCase):
    def test_serialization_obj(self):
        serialize_and_compare_obj(test_number, self)
        serialize_and_compare_obj(test_dict, self)
        serialize_and_compare_obj(test_list, self)

    def test_serialization_func(self):
        serialize_and_compare_func(test_mul, self)
        serialize_and_compare_func(test_fact, self)
        serialize_and_compare_func(test_wrapper, self)
        serialize_and_compare_func(test_vars, self)

    def test_parse_json(self):
        parse_and_compare_func(test_fact, "json", self)
        parse_and_compare_func(test_fact, "jSoN", self)

    def test_parse_yaml(self):
        parse_and_compare_func(test_fact, "yaml", self)
        parse_and_compare_func(test_fact, "YAMl", self)

    def test_parse_toml(self):
        parse_and_compare_func(test_fact, "toml", self)
        parse_and_compare_func(test_fact, "toMl", self)

    def test_multiple_serialization(self):
        s = Serializer()
        ser = s.serialize(test_list)
        ser = s.serialize(ser)
        ser = s.serialize(ser)
        ser = s.serialize(ser)

        res = s.deserialize(ser)
        res = s.deserialize(res)
        res = s.deserialize(res)
        res = s.deserialize(res)

        self.assertEqual(res, test_list)


if __name__ == '__main__':
    unittest.main()
