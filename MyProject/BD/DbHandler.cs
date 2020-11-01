using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.BD
{
    class DbHandler
    {
        public Shop SetShop(string shopId)
        {
            try
            {
                using (var db = new TestDbContext())
                {
                    Console.WriteLine("Поиск магазина в базе данных...");
                    var foundedShop = db.Shops.Find(shopId);
                    if (foundedShop == null)
                    {
                        Console.WriteLine("Магазин не найден, запись в базу данных...");
                        var shop = new Shop() { ShopId = shopId };
                        db.Shops.Add(shop);
                        db.SaveChanges();
                        return shop;
                    }

                    Console.WriteLine("Магазин найден в базе данных.");
                    return foundedShop;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void AddOffers(List<Offer> offers)
        {
            try
            {
                Console.WriteLine("Запись продуктов в базу данных...");
                using (var db = new TestDbContext())
                {
                    db.Database.OpenConnection();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");
                    int i = 0;
                    for (i = 0; i < offers.Count; i++) //Для больших коллекций стараюсь использовать for вместо foreach
                                                       //т.к. обращение по индексу быстрее, чем вызов метода MoveNext().
                                                       //Вызов метода предполагает установку флага возврата из вызваемого метода,
                                                       //а это дополнительные накладные расходы.
                    {
                        var foundedOffer = db.Offers.Find(offers[i].OfferId);

                        if (foundedOffer == null)
                        {
                            db.Offers.Add(offers[i]);
                        }
                        else
                        {
                            db.Offers.Update(foundedOffer);
                        }

                    }

                    Console.WriteLine("Сохранение базы...");
                    db.SaveChanges();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers OFF;");
                    Console.WriteLine("База обновлена.");
                    Console.WriteLine("Записей продуктов добавлено: {0}", i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddOffersToShop(List<Offer> offers, Shop shop)
        {
            try
            {
                Console.WriteLine("Запись продуктов в магазин...");
                using (var db = new TestDbContext())
                {
                    int i = 0;
                    for (i = 0; i < offers.Count; i++)
                    {
                        var foundedOfferInShop = db.Availability.Where(av => av.ShopId == shop.ShopId)
                            .FirstOrDefault(av => av.OfferId == offers[i].OfferId);

                        if (foundedOfferInShop == null)
                        {
                            db.Availability.Add(new AvailabilityInShop() { Offer = offers[i], Shop = shop });
                        }
                        else
                        {
                            db.Availability.Update(foundedOfferInShop);
                        }
                        db.Offers.Update(offers[i]);
                    }
                    db.Shops.Update(shop);

                    Console.WriteLine("Сохранение базы...");
                    db.SaveChanges();
                    Console.WriteLine("База обновлена.");
                    Console.WriteLine("Записей продуктов добавлено: {0}", i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
