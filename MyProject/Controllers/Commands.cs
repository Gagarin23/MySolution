using MyProject.BD;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    class Commands
    {
        private const string searchOffers = "offers";
        public void SaveOffers(string shopId, string url)
        {
            var cancelSrc = new CancellationTokenSource();
            var token = cancelSrc.Token;
            Task.Run(() => SaveOffers(shopId, url, cancelSrc), token);
        }
        public void Print(string shopId)
        {
            var shop = new Shop() 
            { 
                ShopId = shopId
            };

            using(var db = new TestDbContext())
            {
                shop.Offers = db.Offer.Where(offer => offer.ShopId == shop.ShopId).ToArray();
            }

            WriteToConsoleLikeCsv(shop.Offers);
        }

        private void SaveOffers(string shopId, string url, CancellationTokenSource cancelSrc)
        {
            try
            {
                var offers = new OffersGetter(url, searchOffers).Offers?.OfferList;
                var dbHandler = new DbHandler();
                var shop = dbHandler.SetShop(shopId);
                dbHandler.AddOffers(offers, shop);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                cancelSrc.Cancel();
            }
        }
        private void WriteToConsoleLikeCsv(params Offer[] offers)
        {
            foreach (var offer in offers)
            {
                var shopId = offer?.ShopId;
                if (shopId != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("{0};{1};{2}", nameof(offer.OfferId), nameof(offer.Name), nameof(offer.ShopId));
                    Console.WriteLine("{0};{1};{2}", offer.OfferId, offer.Name, shopId);
                }
            }
        }
    }
}
