﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    static class Program
    {
        static void Main(string[] args)
        {      
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MyETL(),
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
