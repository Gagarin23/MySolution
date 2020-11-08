using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExtendedProject.Model
{
    [XmlType("shop")]
    public class Shop
    {
        private string _shopId;
        private List<Offer> _offers = new List<Offer>();

        [Key]
        public string ShopId
        {
            get => _shopId;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length < 50) //Добавить бы ещё валидацию на корректность символов, но это отдельная тема.
                    _shopId = value;

                else
                {
                    Console.WriteLine($"Id магазина не может быть пустым и не должно превышать 49 символов. Значение было \"{value}\"");
                }
            }
        }

        [XmlIgnore]
        public ICollection<Offer> Offers
        {
            get => _offers;
            set => _offers = value as List<Offer>;
        }

        [XmlArray("offers")]
        [NotMapped]
        public List<Offer> XmlOffers
        {
            get => _offers;
            set => _offers = value;
        }

        public override string ToString()
        {
            return _shopId;
        }
    }
}
