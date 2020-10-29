using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    public class Currency
    {
        [XmlAttribute("id")]
        [Key]
        public string CurrencyId { get; set; }

        [XmlAttribute("rate")]
        public decimal Rate { get; set; }
    }
}
