using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class CatcherEventArgs
    {
        public Keys Key { get; }
        public string UnicodeValue { get; }
        public string WindowName { get; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }
        public bool Control { get; set; }
        public bool IsSystemKey { get => CheckIsSystemKey(); }

        public CatcherEventArgs(Keys key, string unicodeValue, string windowName)
        {
            Key = key;
            UnicodeValue = unicodeValue;
            WindowName = windowName;
        }

        bool CheckIsSystemKey()
        {
            return UnicodeValue.Length != 1;
        }
    }
}
