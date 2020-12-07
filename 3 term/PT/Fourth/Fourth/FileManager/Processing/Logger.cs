using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Logger
    {
        LoggingOptions options;

        public Logger(LoggingOptions options)
        {
            this.options = options;
            try
            {
                string name = options.LogPath.Split('\\').Last();
                string path = options.LogPath.Substring(0, options.LogPath.Length - name.Length);
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if(!File.Exists(options.LogPath))
                {
                    File.Create(options.LogPath).Close();
                }
            }
            catch
            {
                this.options.EnableLogging = false;
            }
        }

        public void Log(string message)
        {
            if(options.EnableLogging)
            {
                using(StreamWriter sw = new StreamWriter(options.LogPath, true))
                {
                    sw.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {message}");
                }
            }
        }
    }
}
