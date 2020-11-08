using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtendedProject.BL.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedProject.Model;

namespace ExtendedProject.BL.MSSQL.Tests
{
    [TestClass()]
    public class DbHandlerTests
    {
        [TestMethod()]
        public void DeleteOfferTest()
        {
            var shop = new Shop()
            {
                ShopId = "shop1",
                Offers = new List<Offer>()
                {
                    new Offer()
                    {
                        OfferId = 29984670
                    }
                }
            };
            var dbHandler = new DbHandler();

            using (var db = new TestDbContext())
            {
                dbHandler.DeleteOffer(shop);
            }
        }
    }
}