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
        private int offerId;
        [XmlAttribute("id")]
        [Key]
        public int OfferId
        {
            get => offerId;
            set
            {
                if (value >= 1)
                {
                    try
                    {
                        offerId = checked(value);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                else
                    Console.WriteLine("Id объекта не может быть меньше 1. Значение было {0}", value);
            }
        }

        [XmlElement("name")]
        public string Name { get; set; }

        public string ShopId { get; set; }

        private Shop shop;
        public Shop Shop 
        { 
            get => shop; 
            set
            {
                if (shop != null)
                {
                    Console.WriteLine("Товар с id {0} уже привязан к магазину {1}", OfferId, Shop);
                }
                else
                {
                    shop = value;
                    ShopId = value.ShopId;
                }
            } 
        }

        public override string ToString()
        {
            return OfferId + " " + Name;
        }
    }
}
