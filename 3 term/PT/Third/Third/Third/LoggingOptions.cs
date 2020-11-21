using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third
{
    public class LoggingOptions : Options
    {
        public bool EnableLogging { get; set; } = true;
        public string LogPath { get; set; } = "C:\\FileWatcher\\target\\log.txt";

        public LoggingOptions()
        {

        }
    }
}
