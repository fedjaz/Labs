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
            try
            {
                string name = path.Split('\\').Last();
                path = path.Substring(0, path.Length - name.Length);
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if(!File.Exists(Path))
                {
                    File.Create(Path).Close();
                }
            }
            catch
            {
                IsEnabled = false;
            }
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
