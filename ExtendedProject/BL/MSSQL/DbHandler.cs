using System;
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
                var foundedShop = db.Shops.Where(s => s.ShopId == shop.ShopId).AsNoTracking();

                if (foundedShop.SingleOrDefault() == null)
                    db.Shops.Add(shop);

                else
                {
                    var offers = foundedShop.Select(s => s.Offers).SingleOrDefault() ?? new List<Offer>();
                    shop.Offers = shop.Offers.Where(newO => offers.All(oldO => newO.OfferId != oldO.OfferId)).ToList();
                    db.Update(shop);
                    db.Offers.UpdateRange(shop.Offers);
                }

                db.SaveChanges();
            }
        }

        public void DeleteOffer(Shop shop)
        {
            using (var db = new TestDbContext())
            {
                db.Database.OpenConnection();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");

                var foundedShop = db.Shops.Where(s => s.ShopId == shop.ShopId).AsNoTracking();

                if (foundedShop.SingleOrDefault() == null)
                    return;

                var offers = db.Offers.Where(o => o.OfferId == shop.Offers.First().OfferId);
                var shops = offers.Select(o => o.Shops);

                db.SaveChanges();
            }
        }
    }
}
