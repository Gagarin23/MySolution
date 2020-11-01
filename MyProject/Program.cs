using MyProject.Controllers;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

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
                Console.WriteLine(args[1]);

                if (args[0].Equals("save"))
                    cmd.SaveOffersAsync(args[1], args[2]);

                else if (args[0].Equals("print"))
                    cmd.Print(args[1]);
            }
            else //Debug
            {
                cmd.SaveOffersAsync("test", url);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.DebbugFlagOfEndingAsyncMethod = false;
                cmd.SaveOffersAsync("test2", url);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.Print("test2");
            }

            Console.WriteLine("готово.");
            Console.ReadLine();
        }
    }
}
