using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    public interface IParser
    {
        T DeserializeJson<T>(string json);
        T DeserializeXML<T>(string xml);
        T Map<T>(Dictionary<string, object> dict);
        string SerializeJson(object obj);
        string SerializeXML(object obj);

    }
}
