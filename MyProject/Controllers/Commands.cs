using MyProject.BD;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    class Commands
    {
        private const string searchOffers = "offers";
        public bool DebbugFlagOfEndingAsyncMethod { get; set; }

        /// <summary>
        /// Асинхронный метод сохранения товаров в БД.
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="url"></param>
        public async void SaveOffersAsync(string shopId, string url)
        {
            var cancelSrc = new CancellationTokenSource();
            var token = cancelSrc.Token;
            await Task.Run(() => SaveOffers(shopId, url, cancelSrc), token);
        }

        /// <summary>
        /// Вывод в консоль товаров, находящихся в магазине.
        /// </summary>
        /// <param name="shopId"></param>
        public void Print(string shopId)
        {
            var shop = new Shop()
            {
                ShopId = shopId
            };

            using (var db = new TestDbContext())
            {
                Offer offer;
                AvailabilityInShop availability;
                var tempIds = db.Availability.Where(av => av.ShopId == shop.ShopId).Select(av => av.OfferId).ToArray();
                var offersInCurrentShop = new List<Offer>();

                Console.WriteLine("Первые десять товаров из магазина в виде csv:");
                Console.WriteLine();
                Console.WriteLine("{0};{1};{2}", nameof(offer.OfferId), nameof(offer.Name), nameof(availability.ShopId));
                for (int i = 0; i < 10; i++)
                {
                    offer = db.Offers.Find(tempIds[i]);
                    Console.WriteLine("{0};{1};{2}", offer.OfferId, offer.Name, shopId);
                }
            }


        }

        /// <summary>
        /// Сохранение товаров в БД.
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="url"></param>
        /// <param name="cancelSrc"></param>
        private void SaveOffers(string shopId, string url, CancellationTokenSource cancelSrc)
            // Осознанно нарушен принцип единой ответственности, т.к. того требовало задание,
            // а именно для реализации отношения многие ко многим, запись товаров и наличия товара в магазине
            // требовало различных методов и по сути разных команд. Тут всё хапихано в "save".
        {
            try
            {
                var offers = new OffersGetter(url, searchOffers).Offers?.OfferList;
                var dbHandler = new DbHandler();
                var shop = dbHandler.SetShop(shopId);
                dbHandler.AddOffers(offers);
                dbHandler.AddOffersToShop(offers, shop);
                DebbugFlagOfEndingAsyncMethod = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                cancelSrc.Cancel();
            }
        }
    }
}
