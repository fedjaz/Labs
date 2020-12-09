using System;
using Converter;
using CommonClasses;
using ServiceLayer;
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
            ILogger logger = new Logger(new LoggingOptions());
            ServiceLayer.ServiceLayer SL = new ServiceLayer.ServiceLayer(new DataAccessLayer.Settings.ConnectionOptions(), parser, logger);

            var a = SL.GetPersonInfo(1107);
            string s = parser.SerializeXML(a);
        }
    }
}
