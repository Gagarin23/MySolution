using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    [XmlRoot("offers")]
    public class Offers
    {
        public int OffersId { get; set; }
        [XmlElement("offer")]
        public List<Offer> OfferList { get; set; }
    }
}
