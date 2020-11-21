using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third
{
    public class ETLJsonOptions : ETLOptions
    {
        public ETLJsonOptions(string json) : base()
        {
            ETLOptions options = Converter.Converter.DeserializeJson<ETLOptions>(json);
            ArchiveOptions = options.ArchiveOptions;
            EncryptionOptions = options.EncryptionOptions;
            LoggingOptions = options.LoggingOptions;
            SendingOptions = options.SendingOptions;
            Report = Validator.Validate(this);
        } 
    }
}
