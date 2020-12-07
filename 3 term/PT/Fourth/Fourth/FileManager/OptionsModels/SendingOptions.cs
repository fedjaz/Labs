using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class SendingOptions : Options
    {
        public string SourceDirectory { get; set; } = "C:\\FileWatcher\\source";
        public string TargetDirectory { get; set; } = "C:\\FileWatcher\\target";
        public bool EnableArchiveDirectory { get; set; } = true;
        public string ArchiveDirectory { get; set; } = "C:\\FileWatcher\\target\\archive";


        public SendingOptions()
        {

        }
    }
}
