using ExtendedProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExtendedProject.BL.MSSQL;
using ExtendedProject.BL.XmlDeserializers;

namespace ExtendedProject.Controllers
{
    class Commands
    {
        private const string SearchElement = "shop"; //по условиям задания.
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
            //просто для примера показал синтаксис. Ну и в рабочем приложении
            //я бы запускал запись/чтение в отдельном потоке дабы не вешать вьюшку.
        }

        /// <summary>
        /// Сохранение товаров в БД.
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="url"></param>
        /// <param name="cancelSrc"></param>
        private void SaveOffers(string shopId, string url, CancellationTokenSource cancelSrc)
        {
            try
            {
                IXmlGetElement xmlHandler = new XmlHandler(url);
                var shop = xmlHandler.GetElement<Shop>(SearchElement);
                shop.ShopId = shopId;
                var dbHandler = new DbHandler();
                dbHandler.AddOffers(shop);

                DebbugFlagOfEndingAsyncMethod = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                cancelSrc.Cancel();
            }
        }

        /// <summary>
        /// Вывод в консоль товаров, находящихся в магазине.
        /// </summary>
        /// <param name="shopId"></param>
        public void Print(string shopId)
        {
            using (var db = new TestDbContext())
            {
                var availabilityOffers = db.Shops
                    .Where(s => s.ShopId == shopId)
                    .Select(s => s.Offers)
                    .Select(o => o.Take(10))
                    .SingleOrDefault() ?? new List<Offer>();

                Console.WriteLine("Первые десять товаров из магазина в виде csv:");
                Console.WriteLine();
                Console.WriteLine("{0};{1};{2}", nameof(Offer.OfferId), nameof(Offer.Name), nameof(Shop.ShopId));

                foreach (var offer in availabilityOffers)
                {
                    Console.WriteLine("{0};{1};{2}", offer.OfferId, offer.Name, shopId);
                }

                Console.WriteLine();
            }
        }
    }
}
