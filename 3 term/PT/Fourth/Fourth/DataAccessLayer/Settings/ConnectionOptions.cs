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
        public string User { get; set; } = "DESKTOP - 2D1MIID\\fedjaz";
        public bool IntegratedSecurity { get; set; } = true;
        public ConnectionOptions()
        {

        }
    }
}
