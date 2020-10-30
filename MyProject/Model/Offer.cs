using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    public class Offer
    {
        [XmlAttribute("id")]
        [Key]
        public int OfferId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
