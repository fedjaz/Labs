using System;
using Converter;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new Converter.Converter();
            DataAccessLayer.DataAccessLayer layer = new DataAccessLayer.DataAccessLayer(new DataAccessLayer.Settings.ConnectionOptions(), parser);
            var a = layer.GetAddress(228);
            string s = parser.SerializeJson(a);
        }
    }
}
