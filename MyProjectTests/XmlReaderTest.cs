using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.BD;
using System.Text;
using ExtendedProject.BD;

namespace MyProject.Tests
{
    [TestClass()]
    public class XmlReaderTest
    {
        [TestMethod()]
        public void GetOffersTest()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //Arrange
            var url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
            var searchElement = "offers";

            //Act
            var result = new OffersGetter(url, searchElement);

            //Assert
            Assert.IsNotNull(result.Offers);
        }


    }
}