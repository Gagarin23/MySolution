using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    [XmlRoot("currencies")]
    public class Currencies
    {
        [XmlElement("currency")]
        public List<Currency> CurrencyList { get; set; }
    }
}
