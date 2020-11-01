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
        private static string url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var cmd = new Commands();
            
            if (args.Length > 0)
            {
                cmd = new Commands();
                switch (args[0])
                {
                    case "save":
                        cmd.SaveOffers("ozon", url);
                        break;
                }
            }
            else
            {
                //Debug
                cmd.SaveOffers("test", url);
                Console.WriteLine();
                Console.ReadLine();
                cmd.Print("test");
            }

            Console.ReadLine();
        }
    }
}
