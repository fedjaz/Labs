using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    class Logger
    {
        string Path { get; }
        public bool IsEnabled { get; }

        public Logger(string path, bool isEnabled)
        {
            Path = path;
            IsEnabled = isEnabled;
        }

        public void Log(string message)
        {
            if(IsEnabled)
            {
                using(StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {message}");
                }
            }
        }
    }
}
