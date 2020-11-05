using System;
using System.Text;

namespace PerfomenceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var db = new MyProject.BD.TestDbContext())
            {
                db.Offers.RemoveRange(db.Offers);
                db.Shops.RemoveRange(db.Shops);
                db.Availability.RemoveRange(db.Availability);
                db.SaveChanges();
            }
            using (var db = new ExtendedProject.BD.TestDbContext())
            {
                db.Offers.RemoveRange(db.Offers);
                db.Shops.RemoveRange(db.Shops);
                db.Availability.RemoveRange(db.Availability);
                db.SaveChanges();
            }

            var myProjStartTime = DateTime.Now;
            MyProject.Program.Main(new string[] { });
            var myProjEndTime = DateTime.Now;
            var time1 = (myProjEndTime - myProjStartTime).TotalSeconds;

            var extendedProjectjStartTime = DateTime.Now;
            ExtendedProject.Program.Main(new string[] { });
            var extendedProjectEndTime = DateTime.Now;
            var time2 = (extendedProjectEndTime - extendedProjectjStartTime).TotalSeconds;

            Console.WriteLine();
            Console.WriteLine($"Запись/чтение базы циклами заняло: {time1} секунд");
            Console.WriteLine($"Запись/чтение базы целыми коллекциями заняло: {time2} секунд");


            var perf1 = 100 / time1 * time2;
            var perf2 = 100 / time2 * time1;

            if(perf1 < perf2)
                Console.WriteLine($"работа коллекциями быстрее на {perf1}%");
            else
                Console.WriteLine($"работа циклами быстрее на {perf2}%");

            Console.ReadLine();
        }
    }
}
