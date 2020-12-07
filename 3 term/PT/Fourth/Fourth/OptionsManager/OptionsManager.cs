using System;
using Converter;
using System.IO;

namespace OptionsManager
{
    public class OptionsManager<T> where T : new()
    {
        T DefaultOptions;
        T Json;
        T Xml;
        bool jsonConfigured, xmlConfigured;
        public string Report { get; } = "";

        public OptionsManager(string path, IParser parser, IValidator validator)
        {
            DefaultOptions = new T();
            string options;
            try
            {
                using(StreamReader sr = new StreamReader($"{path}\\config.xml"))
                {
                    options = sr.ReadToEnd();
                }
                Xml = parser.DeserializeXML<T>(options);
                validator.Validate(Xml);
                xmlConfigured = true;
                Report += "Xml options loaded successfully. ";
            }
            catch
            {
                xmlConfigured = false;
            }
            try
            {
                using(StreamReader sr = new StreamReader($"{path}\\appsettings.json"))
                {
                    options = sr.ReadToEnd();
                }
                Json = parser.DeserializeJson<T>(options);
                validator.Validate(Json);
                jsonConfigured = true;
                Report += "Json options loaded successfully. ";
            }
            catch
            {
                jsonConfigured = false;
            }
            if(!jsonConfigured && !xmlConfigured)
            {
                Report = "Failed to load both of json and xml. Using default options and creating appdettings.json";
                if(!File.Exists($"{path}\\appsettings.json"))
                {
                    string json = parser.SerializeJson(DefaultOptions);
                    validator.CreateDirectoryIfNotExist(path);
                    using(StreamWriter sw = new StreamWriter($"{path}\\appsettings.json"))
                    {
                        sw.Write(json);
                    }
                }
                if(!File.Exists($"{path}\\config.xml"))
                {
                    string xml = parser.SerializeXML(DefaultOptions);
                    validator.CreateDirectoryIfNotExist(path);
                    using(StreamWriter sw = new StreamWriter($"{path}\\config.xml"))
                    {
                        sw.Write(xml);
                    }
                }
            }
        }

        public object GetOptions<T>()
        {
            if(jsonConfigured)
            {
                return SeekForOption<T>(Json);
            }
            else if(xmlConfigured)
            {
                return SeekForOption<T>(Xml);
            }
            else
            {
                return SeekForOption<T>(DefaultOptions);
            }
        }

        object SeekForOption<T>(object options)
        {
            if(typeof(T) == DefaultOptions.GetType())
            {
                return options;
            }
            string name = typeof(T).Name;
            try
            {
                return options.GetType().GetProperty(name).GetValue(options, null);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
