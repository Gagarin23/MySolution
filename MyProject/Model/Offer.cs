using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Model
{
    public class Offer
    {
        [XmlAttribute("id")]
        [Key]
        public int OfferId { get; set; }

        [XmlAttribute("avalible")]
        public bool Avalible { get; set; }

        [XmlAttribute("group_id")]
        public int GroupId { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("baseprice")]
        public decimal Baseprice { get; set; }

        [XmlElement("currencyId")]
        public string CurrencyId { get; set; }

        [XmlElement("categoryId")]
        public List<int> CategoryIds { get; set; }

        [XmlElement("picture")]
        public string PictureUrl { get; set; }

        [XmlElement("delivery")]
        public bool Delivery { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("vendor")]
        public string Vendor { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("age")]
        public string Age { get; set; } // Не лучше было бы хранить возраст в базе как byte, а во вьюшке дописывать "+"?
                                        // В xml <age>18+</age>.

        [XmlElement("barcode")]
        public string Barcode { get; set; } // Здесь я решил использовать string т.к.в будущем
                                            // вместо баркодов, возможно будут использовать QR коды.
                                            // На самом деле я пока не знаю, что хуже -
                                            // делать миграцию базы Ozona, если будет переход на QR коды
                                            // или более медленный поиск и больший вес из-за типа string.
                                            // К тому же, баркод может в перспективе выходить за пределы даже long.

        //Увы, я не понял как реализовать список параметров
        //[XmlElement("param")]
        //public OfferParameters OfferParameters { get; set; }
    }
    
    public class OfferParameters
    {
        public enum Params //TODO: переименовать параметры
        {
            [XmlEnum("Альтернативное название")] AnotherName,
            [XmlEnum("Год выпуска")] Year,
            [XmlEnum("Тип носителя")] Parameters1,
            [XmlEnum("Тип упаковки")] Parameters2,
            [XmlEnum("Платформа")] Parameters3,
            [XmlEnum("Возраст потребителя")] Parameters4,
            [XmlEnum("Версия ОС")] Parameters5,
            [XmlEnum("Язык интерфейса")] Parameters6,
        }

        [XmlAttribute("name")]
        public Params Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
