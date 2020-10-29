using MyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

            var uri = new Uri(@"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml");
            string url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
            var reader = new TestReader<Offers>("offers", LoadXml);
            var offers = reader.GetModelObject(url);
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
    }
}
