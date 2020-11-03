using System.Collections.Generic;
using System.Xml.Serialization;
using MyProject.Model;

namespace MyProject
{
    /// <summary>
    /// Сурогатный класс для чтения предоставленной xml.
    /// </summary>
    [XmlRoot("offers")]
    public class SurrogateOffers
    {
        [XmlElement("offer")]
        public List<Offer> OfferList { get; set; }
    }
}
