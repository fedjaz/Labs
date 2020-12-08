using System;
using Converter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataAccessLayer
{
    class Validator : OptionsManager.IValidator
    {
        public void Validate(object obj)
        {
            Settings.DataAccessOptions options = obj as Settings.DataAccessOptions;
            #region Sending validation
            if(options.SendingOptions.BatchSize < 1)
            {
                options.SendingOptions.BatchSize = 100;
            }
            if(!CreateDirectoryIfNotExist(options.SendingOptions.Target))
            {
                options.SendingOptions.Target = "C:\\FileWatcher\\source";
                CreateDirectoryIfNotExist(options.SendingOptions.Target);
            }
            if((int)options.SendingOptions.PullingMode < 0 || (int)options.SendingOptions.PullingMode >= 2)
            {
                options.SendingOptions.PullingMode = Settings.SendingOptions.PullingModes.FullTable;
            }
            #endregion
        }

        public bool CreateDirectoryIfNotExist(string path)
        {
            try
            {
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool CreateFileIfNotExist(string path)
        {
            try
            {
                string name = path.Trim('\\').Split('\\').Last();
                string p = path.Substring(0, path.Length - name.Length);
                if(!Directory.Exists(p))
                {
                    Directory.CreateDirectory(p);
                }
                if(!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
