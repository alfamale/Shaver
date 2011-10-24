using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyByNight;
using FlyByNight.Controllers;
using FlyByNight.Tests.Helpers;
using System.Web;
using System.IO;
using Moq;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Fakes;
using System.Web.Routing;

namespace FlyByNight.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(HomeController.IndexMessage, result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
