using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.BD
{
    class DbHandler
    {
        /// <summary>
        /// Установить магазин для работы с ним в БД.
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Запись товаров в БД.
        /// </summary>
        /// <param name="offers"></param>
        public void AddOffers(IList<Offer> offers)
        {
            try
            {
                Console.WriteLine("Запись продуктов в базу данных...");
                using (var db = new TestDbContext())
                {
                    db.Database.OpenConnection();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");
                    int i = 0;
                    for (i = 0; i < offers.Count; i++) 
                        //Для больших коллекций стараюсь использовать for вместо foreach
                        //т.к. обращение по индексу быстрее, чем вызов метода MoveNext() в foreach.
                        //Вызов метода предполагает в стеке установку флага возврата из вызваемого метода,
                        //а это дополнительные накладные расходы.
                    {
                        var foundedOffer = db.Offers.Find(offers[i].OfferId);

                        //var foundedOffers = db.Offers.Intersect(offers);

                        //if(foundedOffers != null)
                        //{
                        //    var elementOtAdd = foundedOffers.Except(offers);
                        //    db.Offers.AddRange(elementOtAdd);
                        //}
                        //else
                        //{
                        //    db.Offers.AddRange(offers);
                        //}
                        // Закомменчиный код выше я хотел использовать чтобы не прибегать к циклам,
                        // но в момент расчёта .Intersect(offers) вылетает исключение.
                        // Гугл говорит, что не удаётся преобразовать .Intersect(offers) в запрос sql.
                        // Как вариант я мог бы загрузить всю DbSet в память,
                        // но как по мне это слишком затратно по ресурсам.

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
                    Console.WriteLine("Записей обработано: {0}", i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Запись товаров в магазин.
        /// </summary>
        /// <param name="offers"></param>
        /// <param name="shop"></param>
        public void AddOffersToShop(IList<Offer> offers, Shop shop)
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
                    Console.WriteLine("Записей обработано: {0}", i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Метод для юнит теста.
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public void TestDbHandler(Offer offer, string shopId)
        {
            var shop = SetShop(shopId);
            var list = new List<Offer>() { offer };
            AddOffers(list);
            AddOffersToShop(list, shop);
            
        }
    }
}
