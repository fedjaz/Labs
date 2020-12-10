using System;
using Converter;
using CommonClasses;
using OptionsManager;
using ServiceLayer;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Settings;
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
            IValidator validator = new Validator();
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            OptionsManager<DataAccessOptions> options =
                new OptionsManager<DataAccessOptions>(directory, parser, validator);

            LoggingOptions loggingOptions = new LoggingOptions();
            loggingOptions.ConnectionOptions = options.GetOptions<ConnectionOptions>() as ConnectionOptions;
            loggingOptions.EnableLogging = true;
            ILogger logger = new Logger(loggingOptions, parser);
            ServiceLayer.ServiceLayer SL = new ServiceLayer.ServiceLayer(
                options.GetOptions<ConnectionOptions>() as ConnectionOptions,
                parser, logger);

            SendingOptions sendingOptions = options.GetOptions<SendingOptions>() as SendingOptions;

            logger.Log("Starting pulling data");
            if(sendingOptions.PullingMode == SendingOptions.PullingModes.ByBatches)
            {
                int curIndex = 1;
                int maxID = SL.DAL.PersonMaxID();
                List<PersonInfo> info;
                while(curIndex < maxID)
                {
                    info = SL.GetPersonsRange(curIndex, sendingOptions.BatchSize);
                    int lastID = info.Last().Person.BusinessEntityID;
                    string s = parser.SerializeXML(info);
                    using(StreamWriter sw = new StreamWriter($"{sendingOptions.Target}\\file{curIndex}-{lastID}.txt"))
                    {
                        sw.Write(s);
                    }
                    curIndex = lastID + 1;
                }
            }
            else if(sendingOptions.PullingMode == SendingOptions.PullingModes.FullTable)
            {
                List<PersonInfo> info = SL.GetPersons();
                SplitOnBatches(info, sendingOptions, parser);
            }
            else if(sendingOptions.PullingMode == SendingOptions.PullingModes.FullJoin)
            {
                List<PersonInfo> info = SL.GetPersonsByJoin();
                SplitOnBatches(info, sendingOptions, parser);
            }

            logger.Log("Pulled all data successfully");
        }

        static void SplitOnBatches(List<PersonInfo> info, SendingOptions sendingOptions, IParser parser)
        {
            int curIndex = 0;
            while(curIndex < info.Count)
            {
                List<PersonInfo> subInfo = info.GetRange(curIndex,
                                                         Math.Min(sendingOptions.BatchSize, info.Count - curIndex));

                int firstID = subInfo.First().Person.BusinessEntityID;
                int lastID = subInfo.Last().Person.BusinessEntityID;
                string s = parser.SerializeXML(subInfo);
                using(StreamWriter sw = new StreamWriter($"{sendingOptions.Target}\\file{firstID}-{lastID}.txt"))
                {
                    sw.Write(s);
                }
                curIndex += sendingOptions.BatchSize;
            }
        }
    }
}
