﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
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