using ExtendedProject.Controllers;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

[assembly: InternalsVisibleTo("MyProjectTests")]
namespace ExtendedProject
{
    class Program
    {
        private static string url = @"http://static.ozone.ru/multimedia/yml/facet/mobile_catalog/1133677.xml";
        public static void Main(string[] args)
        {
            Test.Run();
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
                cmd.SaveOffersAsync("test", url);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.DebbugFlagOfEndingAsyncMethod = false;
                cmd.SaveOffersAsync("test2", url);
                while (!cmd.DebbugFlagOfEndingAsyncMethod) Thread.Sleep(1000);
                cmd.Print("test2");
            }
        }
    }
}
