using MyProject.BD;
using MyProject.Controllers;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("MyProjectTests")]
namespace MyProject
{
    class Program
    {
        private static string _url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
        private static string _searchElement = "offers";
        private static string _shopId = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqq";
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            DeleteDb();

            var offers = new OffersGetter(_url, _searchElement).Offers.OfferList;

            var dbHandler = new DbHandler();
            dbHandler.AddOffers(offers, _shopId);

            var ids = offers.Select(offer => offer.OfferId).ToArray();
            var rnd = new Random();
            var randomId = ids[rnd.Next(0, ids.Length - 1)];
            var offer = dbHandler.GetDbObject(GetOffer, randomId);

            //WriteToConsole(offer);

            Console.ReadLine();
        }

        static void DeleteDb()
        {
            using(var db = new TestDbContext())
            {
                var allOffers = db.Offer.Select(offer => offer);
                db.RemoveRange(allOffers);
                db.SaveChanges();
            }
        }

        public static Offer GetOffer(TestDbContext db, int id)
        {
            return db.Offer.Find(id);
        }

        public static SurrogateOffers LoadOffers(string xml)
        {
            var serializer = new XmlSerializer(typeof(SurrogateOffers));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (SurrogateOffers)serializer.Deserialize(ms); // Запаковка в object при возврате крайне не радует :(
            }
        }

        public static void WriteToConsole(Offer offer)
        {
            var ids = offer?.GetShopsId();
            if (offer != null && ids != null)
            {
                Console.WriteLine();
                Console.WriteLine("Случайный объект из базы в формате csv:");
                Console.WriteLine("OfferId;Name;ShopsId");
                var sb = new StringBuilder();
                foreach(var id in ids)
                {
                    sb.Append(id + " ");
                }
                Console.WriteLine("{0};{1};{2}", offer.OfferId, offer.Name, sb.ToString());
            }
        }
    }
}
