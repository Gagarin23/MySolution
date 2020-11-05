using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.Model;
using System.Linq;

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
            using (var db = new TestDbContext())
            {
                result = db.Availability.SingleOrDefault(av => av.ShopId == shopId && av.OfferId == offer.OfferId);
            }

            //Assert
            Assert.AreEqual(offer.OfferId, result?.OfferId);

            if (result != null)
                dbHandler.RemoveTestItems(offer, shopId);
        }
    }
}