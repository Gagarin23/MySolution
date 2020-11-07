using ExtendedProject.BD;
using ExtendedProject.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExtendedProject.Controllers
{
    class Commands
    {
        private const string SearchOffers = "offers"; //по заданию должен загружать товары.
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
        /// Вывод в консоль товаров, находящихся в магазине.
        /// </summary>
        /// <param name="shopId"></param>
        public void Print(string shopId)
        {
            using (var db = new TestDbContext())
            {
                var availabilityOffers = db.Availability.Where(av => av.ShopId == shopId).Take(10).ToArray();

                Console.WriteLine("Первые десять товаров из магазина в виде csv:");
                Console.WriteLine();
                Console.WriteLine("{0};{1};{2}", nameof(Offer.OfferId), nameof(Offer.Name), nameof(AvailabilityInShop.ShopId));

                for (int i = 0; i < availabilityOffers.Length; i++)
                {
                    var offer = db.Offers.Find(availabilityOffers[i].OfferId);
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
                var offers = new OffersGetter(url, SearchOffers).Offers?.OfferList;
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
