using MyProject.BD;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
            var reader = new TestReader<Offers>("offers", LoadXml);
            var offers = reader.GetModelObject(url);

            DbHandler.SaveOffer(offers.OfferList, 1);

            var ids = offers.OfferList.Select(offer => offer.OfferId).ToArray();
            var rnd = new Random();
            var randomId = ids[rnd.Next(0, ids.Length - 1)];
            var offer = DbHandler.ReadOffers(randomId);

            WriteToConsole(offer);

            Console.ReadLine();
        }

        public static Offers LoadXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(Offers));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (Offers)serializer.Deserialize(ms); // Запаковка в object при возврате крайне не радует :(
            }
        }

        public static void WriteToConsole(Offer offer)
        {
            Console.WriteLine("{0};{1};{2}", nameof(offer.OfferId), nameof(offer.Name), nameof(offer.ShopId));
            Console.WriteLine("{0};{1};{2}", offer.OfferId, offer.Name.Substring(0, 50), offer.ShopId);
        }
    }
}
