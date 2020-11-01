using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BD
{
    class DbHandler
    {
        public void AddOffers(List<Offer> offers, string shopId)
        {
            try
            {
                Console.WriteLine("Запись в базу данных...");
                using (var db = new TestDbContext())
                {
                    var shop = AddShop(shopId, db);
                    db.Database.OpenConnection();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offer ON;");

                    for (int i = 0; i < offers.Count; i++) //Для больших коллекций стараюсь использовать for вместо foreach
                                                           //т.к. обращение по индексу быстрее, чем вызов метода MoveNext().
                                                           //Вызов метода предполагает установку флага возврата из вызваемого метода
                                                           //а это дополнительные накладные расходы.
                    {
                        //offers[i].AddShop(shop);

                        if (db.Offer.Find(offers[i].OfferId) == null)
                        {
                            db.Offer.Add(offers[i]);
                            Console.WriteLine("Объект с id: {0} добавлен в базу.", offers[i].OfferId);
                        }
                        else
                        {
                            Console.WriteLine("Объект с ключом {0}, уже существует в базе!", offers[i].OfferId);
                        }
                    }

                    Console.WriteLine("Сохранение базы...");
                    db.SaveChanges();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offer OFF;");

                    Console.WriteLine("База обновлена.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Shop AddShop(string shopId, TestDbContext db)
        {
            var foundedShop = db.Shop.Find(shopId);
            if (foundedShop == null)
            {
                var shop = new Shop() { ShopId = shopId };
                db.Shop.Add(shop);
                db.SaveChanges();
                return shop;
            }

            return foundedShop;
        }

        public T GetDbObject<T>(Func<TestDbContext, int, T> getDbObject, int id)
            where T : class
        {
            try
            {
                using (var db = new TestDbContext())
                {
                    return getDbObject(db, id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public T[] GetDbObject<T>(Func<TestDbContext, T[]> getDbObjects)
            where T : class
        {
            try
            {
                using (var db = new TestDbContext())
                {
                    return getDbObjects(db);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
