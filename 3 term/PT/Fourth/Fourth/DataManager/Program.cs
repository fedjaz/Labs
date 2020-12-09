using System;
using Converter;
using CommonClasses;
using OptionsManager;
using ServiceLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new Converter.Converter();
            ILogger logger = new Logger(new LoggingOptions());
            IValidator validator = new Validator();
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            OptionsManager<DataAccessLayer.Settings.DataAccessOptions> options =
                new OptionsManager<DataAccessLayer.Settings.DataAccessOptions>(directory, parser, validator);
            
            ServiceLayer.ServiceLayer SL = new ServiceLayer.ServiceLayer(
                options.GetOptions<DataAccessLayer.Settings.ConnectionOptions>() as DataAccessLayer.Settings.ConnectionOptions,
                parser, logger);

            DataAccessLayer.Settings.SendingOptions sendingOptions = 
                options.GetOptions<DataAccessLayer.Settings.SendingOptions>() as DataAccessLayer.Settings.SendingOptions;

            if(sendingOptions.PullingMode == DataAccessLayer.Settings.SendingOptions.PullingModes.FullTable)
            {
                int curIndex = 0;
                int size = SL.DAL.PersonCount();
                List<DataAccessLayer.Models.PersonInfo> info = null;
                while(curIndex < size)
                {
                    info = SL.GetPersonsRange(curIndex, sendingOptions.BatchSize);
                    int lastID = info.Last().Person.BusinessEntityID;
                    string s = parser.SerializeXML(info);
                    using(StreamWriter sw = new StreamWriter($"{sendingOptions.Target}\\file{curIndex}-{lastID}.txt"))
                    {
                        sw.Write(s);
                    }
                    curIndex += sendingOptions.BatchSize;
                }
            }            
        }
    }
}
