using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace ExtendedProject.Model
{
    [XmlType("offer")]
    public class Offer : IEquatable<Offer>
    {
        private int _offerId;
        [XmlAttribute("id")]
        [Key]
        public int OfferId
        {
            get => _offerId;
            set
            {
                if (value >= 1)
                {
                    try
                    {
                        _offerId = checked(value);
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

        private string _name;
        [XmlElement("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                else
                {
                    Console.WriteLine($"Имя товара не может быть пустым. Значение было \"{value}\"");
                }
            }
        }

        [XmlIgnore]
        public ICollection<Shop> Shops { get; set; } = new List<Shop>();

        public override string ToString()
        {
            return OfferId + " " + Name;
        }

        public bool Equals(Offer other)
        {
            if (other == null)
                return false;

            return _offerId == other.OfferId;
        }

        public override bool Equals(object obj)
        {
            if (obj is Offer other)
                return _offerId == other.OfferId;

            return false;
        }

        public override int GetHashCode()
        {
            return _offerId.GetHashCode();
        }
    }
}
