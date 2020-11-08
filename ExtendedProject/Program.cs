using ExtendedProject.Controllers;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ExtendedProject.BL.MSSQL;

[assembly: InternalsVisibleTo("ExtendedProjectTests")]
namespace ExtendedProject
{
    class Program
    {
        private static string url1 = @"http://static.ozone.ru/multimedia/yml/facet/mobile_catalog/1133677.xml";
        private static string url2 = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
        private static string url3 = @"http://static.ozone.ru/multimedia/yml/facet/business.xml";
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var cmd = new Commands();

            if (args.Length > 0)
            {
                if (args[0].Equals("save"))
                    cmd.SaveOffersAsync(args[1], args[2]);

                else if (args[0].Equals("print"))
                    cmd.Print(args[1]);
            }
            else //Debug
            {
                //new TestDbContext(true);
                cmd.SaveOffersAsync("shop1", url1);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.DebbugFlagOfEndingAsyncMethod = false;
                cmd.SaveOffersAsync("shop2", url2);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.DebbugFlagOfEndingAsyncMethod = false;
                cmd.SaveOffersAsync("shop3", url3);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.Print("shop1");
                cmd.Print("shop2");
                cmd.Print("shop3");
            }
        }
    }
}
