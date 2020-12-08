using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses
{
    public class Logger : ILogger
    {
        public LoggingOptions LoggingOptions { get; set; }

        public void Log(string message)
        {

        }

        public Logger(LoggingOptions loggingOptions)
        {
            LoggingOptions = loggingOptions;
        }
    }
}
