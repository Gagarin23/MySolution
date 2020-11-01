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
                    Console.WriteLine("Id объекта не может быть меньше 1.");
            }
        }

        [XmlElement("name")]
        public string Name { get; set; }
        private List<string> shopsId;
        private List<Shop> shops;

        public void AddShop(Shop shop)
        {
            if (shopsId == null)
            {
                shopsId = new List<string>() { shop.ShopId };
                shops = new List<Shop>() { shop };
            }
            else if (shopsId.Contains(shop.ShopId))
            {
                Console.WriteLine("Товар с id {0} уже привязан к магазину {1}", OfferId, shops);
            }

            if (!shopsId.Contains(shop.ShopId))
            {
                shopsId.Add(shop.ShopId);
                shops.Add(shop);
            }

        }

        public List<string> GetShopsId() => shopsId;
        public List<Shop> GetShops() => shops;

        public override string ToString()
        {
            return OfferId + " " + Name;
        }
    }
}
