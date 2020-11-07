using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace ExtendedProject.Model
{
    public sealed class Offer
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

        public override string ToString()
        {
            return OfferId + " " + Name;
        }
    }
}
