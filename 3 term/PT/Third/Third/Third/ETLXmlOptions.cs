using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third
{
    public class ETLXmlOptions : ETLOptions
    {
        public ETLXmlOptions(string xml) : base()
        {
            ETLOptions options = Converter.Converter.DeserializeXML<ETLOptions>(xml);
            ArchiveOptions = options.ArchiveOptions;
            EncryptionOptions = options.EncryptionOptions;
            SendingOptions = options.SendingOptions;
            LoggingOptions = options.LoggingOptions;
            Report = Validator.Validate(this);
        }
    }
}
