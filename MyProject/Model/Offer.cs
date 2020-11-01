using System;
using System.ComponentModel.DataAnnotations;
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

        public override string ToString()
        {
            return OfferId + " " + Name;
        }
    }
}
