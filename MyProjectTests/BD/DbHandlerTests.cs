using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.BD;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BD.Tests
{
    [TestClass()]
    public class DbHandlerTests
    {
        [TestMethod()]
        public void TestDbHandlerTest()
        {
            //Arrange
            var offer = new Offer() { OfferId = 999999999, Name = "testProduct" };
            var shopId = "testShop";

            //Act
            var dbHandler = new DbHandler();
            dbHandler.TestDbHandler(offer, shopId);

            AvailabilityInShop result;
            using(var db = new TestDbContext())
            {
                result = db.Availability.Where(av => av.ShopId == shopId)
                    .FirstOrDefault(av => av.OfferId == offer.OfferId);
            }

            //Assert
            Assert.IsNotNull(result);
        }
    }
}