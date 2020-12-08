using System;
using CommonClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileManager
{
    public class ETLOptions : Options
    {
        public SendingOptions SendingOptions { get; set; } = new SendingOptions();
        public EncryptionOptions EncryptionOptions { get; set; } = new EncryptionOptions();
        public CommonClasses.LoggingOptions LoggingOptions { get; set; } = new CommonClasses.LoggingOptions();
        public ArchiveOptions ArchiveOptions { get; set; } = new ArchiveOptions();

        [Converter.JsonIgnore]
        [Converter.XMLIgnore]
        public string Report { get; protected set; } = "";
        public ETLOptions()
        {

        }
    }
}
