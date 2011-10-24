/*  Copyright Andrew Woodcock, 2011

    This file is part of Shaver.

    Shaver is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Shaver is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with Shaver.  If not, see <http://www.gnu.org/licenses/>. 
 */

using FlyByNight.Controllers;
using FlyByNight.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using MvcContrib.TestHelper;

namespace FlyByNight.Tests.Controllers
{
    
    
    /// <summary>
    ///This is a test class for ParcelsControllerTest and is intended
    ///to contain all ParcelsControllerTest Unit Tests
    ///</summary>
    [TestClass]
    public class ParcelsControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        public ParcelsController ParcelsControllerHelper()
        {
            var controller = new ParcelsController();

            // MVCContrib TestHelper is initializing the Controller for us INCLUDING session state!
            var builder = new TestControllerBuilder();
            builder.InitializeController(controller);

            return controller;
        }

        public ParcelModel ParcelModelHelper()
        {
            return new ParcelModel
            {
                Sender = "Sender",
                SenderAddress = "Sender Address",
                Receiver = "Receiver",
                ReceiverAddress = "Receiver Address",
                Message = "Message",
                Description = "Description"
            };
        }

        /// <summary>
        ///A test for AddParcel
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void AddParcelTest()
        {
            var target = ParcelsControllerHelper();
            var expected = ParcelModelHelper();
            var id = expected.Parcel;

            target.AddParcel(expected);

            var actual = target.GetParcel(id);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for Create
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void CreateTest()
        {
            var target = ParcelsControllerHelper();
            var actual = target.Create();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void CreateTest_AddNewParcel()
        {
            var target = ParcelsControllerHelper();
            var parcel = ParcelModelHelper();
            var expected = target.GetParcels().Count + 1;
            var actionResult = target.Create(parcel);
            var actual = target.GetParcels().Count;

            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(actionResult);
        }

        /// <summary>
        ///A test for Details
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void DetailsTest()
        {
            var target = ParcelsControllerHelper();
            var id = target.GetParcels()[0].Parcel;
            var actual = target.Details(id);

            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetParcel
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void GetParcelTest()
        {
            var target = ParcelsControllerHelper();
            var expected = target.GetParcels()[0];
            var id = expected.Parcel;
            var actual = target.GetParcel(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetParcels
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void GetParcelsTest()
        {
            var parcel = ParcelModelHelper();
            var id = parcel.Parcel;
            var target = ParcelsControllerHelper();

            var expected = target.GetParcels().Count + 1;

            target.AddParcel(parcel);
            var actual = target.GetParcels();

            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void IndexTest()
        {
            var target = ParcelsControllerHelper();
            var actual = target.Index();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Setup
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void SetupTest()
        {
            var target = ParcelsControllerHelper();
            const int expected = 2;
            target.Setup();
            var actual = target.GetParcels().Count;
            Assert.AreEqual(expected, actual);
        }

    }
}
