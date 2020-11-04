using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MyProject.Model
{
    public class Offer
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

        private string name;
        [XmlElement("name")]
        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    name = value;
                }
                else
                {
                    Console.WriteLine($"Имя товара не может быть пустым. Значение было \"{value}\"");
                }
            }
        }

        public override string ToString()
        {
            return OfferId + " " + Name;
        }
    }
}
