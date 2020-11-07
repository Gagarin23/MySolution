using ExtendedProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtendedProject.BD
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
            Console.WriteLine();
            if (string.IsNullOrEmpty(shopId))
                throw new NullReferenceException("Название магазина не может быть пустым!");

            using (var db = new TestDbContext())
            {
                Console.WriteLine("Поиск магазина в базе...");
                var foundedShop = db.Shops.Find(shopId);
                if (foundedShop == null)
                {
                    Console.WriteLine("Магазин не найден, запись в базу...");
                    var shop = new Shop() { StringId = shopId };
                    db.Shops.Add(shop);
                    db.SaveChanges();
                    return shop;
                }

                Console.WriteLine("Магазин найден в базе.");
                return foundedShop;
            }
        }

        /// <summary>
        /// Запись товаров в БД.
        /// </summary>
        /// <param name="offers"></param>
        public void AddOffers(IList<Offer> offers)
        {
            Console.WriteLine();
            if (offers == null || offers.Count == 0)
                throw new NullReferenceException("Список товаров не может быть пустым.");

            Console.WriteLine("Запись продуктов в базу...");
            using (var db = new TestDbContext())
            {
                var oldOffers = db.Offers;
                var difference = offers
                    .Where(newO => oldOffers.All(oldO => newO.OfferId != oldO.OfferId));

                db.Offers.AddRange(difference);

                db.Database.OpenConnection();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");

                Console.WriteLine("Сохранение базы...");
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers OFF;");
                Console.WriteLine("База сохранена.");
            }
        }

        /// <summary>
        /// Запись товаров в магазин.
        /// </summary>
        /// <param name="offers"></param>
        /// <param name="shop"></param>
        public void AddOffersToShop(IList<Offer> offers, Shop shop)
        {
            Console.WriteLine();
            if (offers == null || offers.Count == 0 || shop == null)
                throw new NullReferenceException("Список товаров не может быть пустым.");

            Console.WriteLine("Запись продуктов в магазин...");
            using (var db = new TestDbContext())
            {
                var oldAvailability = db.Availability;

                //Получаем товаровы, которых нет в магазине
                var difference = offers.Where(o => oldAvailability
                    .All(oldAv => o.OfferId != oldAv.OfferId || oldAv.ShopId != shop.StringId));

                //На основе товаров, которых нет в магазин,
                //получаем список объектов "наличия товара" в базе.
                var availabilityToAdd = difference.Select(o => new AvailabilityInShop { Offer = o, Shop = shop });

                db.Availability.AddRange(availabilityToAdd);

                db.Shops.Update(shop);
                db.Offers.UpdateRange(difference);

                Console.WriteLine("Сохранение базы...");
                db.SaveChanges();
                Console.WriteLine("База обновлена.");
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

        public void RemoveTestItems(Offer offer, string shopId)
        {
            using (var db = new TestDbContext())
            {
                var foundedAvaibility = db.Availability
                    .SingleOrDefault(av => av.ShopId == shopId && av.OfferId == offer.OfferId);

                if (foundedAvaibility != null)
                    db.Availability.Remove(foundedAvaibility);

                var foundedOffer = db.Offers.Find(offer.OfferId);

                if (foundedOffer != null)
                    db.Offers.Remove(foundedOffer);

                var foundedShop = db.Shops.Find(shopId);

                if (foundedShop != null)
                    db.Shops.Remove(foundedShop);
            }
        }
    }
}
