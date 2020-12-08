using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses
{
    public class LoggingOptions
    {
        public DataAccessLayer.Settings.ConnectionOptions ConnectionOptions { get; set; } = new DataAccessLayer.Settings.ConnectionOptions();
        public bool EnableLogging { get; set; } = true;
        public LoggingOptions()
        {

        }
    }
}
