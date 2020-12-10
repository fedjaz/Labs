using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsManager
{
    public interface IValidator
    {
        void Validate(object obj);
        bool CreateDirectoryIfNotExist(string path);
        bool CreateFileIfNotExist(string path);
    }
}
