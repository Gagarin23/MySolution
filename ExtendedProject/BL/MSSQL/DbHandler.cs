using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtendedProject.Model;
using Microsoft.EntityFrameworkCore;

namespace ExtendedProject.BL.MSSQL
{
    class DbHandler
    {
        public void AddOffers(Shop shop)
        {
            using (var db = new TestDbContext())
            {
                db.Database.OpenConnection();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");

                var foundedShop = db.Shops
                    .Include(s => s.Offers)
                    .SingleOrDefault(s => s.ShopId == shop.ShopId);

                if (foundedShop == null)
                    db.Shops.Add(shop);

                else
                {
                    var difference = shop.Offers.Except(foundedShop.Offers).ToArray();
                    foreach (var newOffer in difference)
                    {
                        foundedShop.Offers.Add(newOffer);
                    }

                    try
                    {
                        db.Offers.AddRange(difference);
                    }
                    catch
                    {
                        var existingOffers = db.Offers
                            .AsEnumerable()
                            .Intersect(shop.Offers).ToArray();
                        difference = shop.Offers.Except(existingOffers).ToArray();
                        db.Offers.AddRange(difference);
                    }
                }
                db.SaveChanges();
            }
        }

        public void AddOffers(ICollection<Offer> offers, string shopId)
        {
            var shop = new Shop()
            {
                ShopId = shopId,
                Offers = offers
            };
            AddOffers(shop);
        }
    }
}
