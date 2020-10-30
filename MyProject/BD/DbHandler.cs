using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BD
{
    class DbHandler
    {
        public static void SaveOffer(List<Offer> offers, int shopId)
        {
            try
            {
                Console.WriteLine("Запись в базу данных...");
                using (var db = new TestDbContext())
                {
                    AddShop(shopId, db);

                    for (int i = 0; i < offers.Count; i++) //Для больших коллекций стараюсь использовать for вместо foreach
                                                           //т.к. обращение по индексу быстрее вызова метода MoveNext().
                                                           //Вызов метода всегда предполагает установку флага в стеке
                                                           //для установки точки возврата из вызваемого метода.
                    {
                        offers[i].ShopId = shopId;
                        var foundedOffer = db.Offer.Find(offers[i].OfferId);

                        if (foundedOffer == null)
                        {
                            db.Offer.Add(offers[i]);
                            db.Database.OpenConnection();
                            db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offer ON;");
                        }
                        else
                        {
                            db.Offer.Remove(foundedOffer);
                            db.Offer.Add(offers[i]);
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

        private static void AddShop(int shopId, TestDbContext db)
        {
            if (db.Shop.Find(shopId) == null)
            {
                var shop = new Shop() { ShopId = shopId };
                db.Shop.Add(shop);
                db.Database.OpenConnection();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Shop ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Shop OFF;");
            }
        }

        public static Offer ReadOffers(int id)
        {
            try
            {
                using (var db = new TestDbContext())
                {
                    return db.Offer.Find(id);
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
