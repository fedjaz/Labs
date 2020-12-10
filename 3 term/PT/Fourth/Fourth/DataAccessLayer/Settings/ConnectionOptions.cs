using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Settings
{
    public class ConnectionOptions
    {
        public string DataSource { get; set; } = "DESKTOP-2D1MIID\\AdventureWorks";
        public string Database { get; set; } = "AdventureWorks2017";
        public string User { get; set; } = "WindowsService";
        public string Password { get; set; } = "helloworld";
        public bool IntegratedSecurity { get; set; } = false;
        public ConnectionOptions()
        {

        }
    }
}
