using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtendedProject.Model;
using System.Linq;
using ExtendedProject.BL.MSSQL;

namespace MyProject.BD.Tests
{
    [TestClass()]
    public class DbHandlerTests
    {
        [TestMethod()]
        public void TestDbHandlerTest()
        {
            //Arrange
            var offer = new Offer() {OfferId = 999999999, Name = "testProduct"};
            var shopId = "testShop";

            //Act
            var dbHandler = new DbHandler();
        }
    }
}