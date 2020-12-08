using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Settings
{
    public class SendingOptions
    {
        public string Target { get; set; } = "C:\\FileWatcher\\source";
        public int BatchSize { get; set; } = 100;
        public PullingModes PullingMode { get; set; } = PullingModes.FullTable;
        
        public enum PullingModes
        {
            SingleLine,
            FullTable,
            FullJoin
        }

        public SendingOptions()
        {

        }
    }
}
