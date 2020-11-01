using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject;
using MyProject.BD;
using MyProject.Controllers;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Tests
{
    [TestClass()]
    public class MyControllerTests
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