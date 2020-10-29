using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    [XmlRoot("orderingTime")]
    class OrderingTime
    {
        [XmlElement("ordering")]
        public List<Ordering> OfferLocation { get; set; }
    }
}
