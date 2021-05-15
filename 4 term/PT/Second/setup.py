from setuptools import setup

setup(
    name="fedjazserializer",
    packages=[
        "fedjaz_serializer",
        "fedjaz_serializer/serializer",
        "fedjaz_serializer/parser",
        "fedjaz_serializer/parser/parsers",
        "fedjaz_serializer/parser/parsers/json",
        "fedjaz_serializer/parser/parsers/yaml",
        "fedjaz_serializer/parser/parsers/toml",
    ],
    version="1.0.0",
    author="fedjaz",
    scripts=["bin/fedjazserializer.py"]
)