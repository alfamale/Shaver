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

using FlyByNight.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace FlyByNight.Tests.Models
{
    
    
    /// <summary>
    ///This is a test class for ParcelModelTest and is intended
    ///to contain all ParcelModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class ParcelModelTest
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


        /// <summary>
        ///A test for ParcelModel Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void ParcelModelConstructorTest()
        {
            var expectedDeliveryDate = DateTime.Now.AddDays(2);
            const double expectedCost = 9.95;
            var target = new ParcelModel();
            Assert.IsNotNull(target.Parcel);
            Assert.AreEqual(expectedDeliveryDate, target.DeliveryDate);
            Assert.AreEqual(expectedCost, target.Cost);
            Assert.IsNull(target.Description);
            Assert.IsNull(target.Message);
            Assert.IsNull(target.Receiver);
            Assert.IsNull(target.ReceiverAddress);
            Assert.IsNull(target.Sender);
            Assert.IsNull(target.SenderAddress);
        }

        /// <summary>
        ///A test for Cost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void CostTest()
        {
            const double expected = 9.95;
            var target = new ParcelModel();
            var actual = target.Cost;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeliveryDate
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void DeliveryDateTest()
        {
            var expected = DateTime.Now.AddDays(2);
            var target = new ParcelModel();
            var actual = target.DeliveryDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void DescriptionTest()
        {
            const string expected = "Description";
            var target = new ParcelModel()
            {
                Description = expected
            };

            var actual = target.Description;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Message
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void MessageTest()
        {
            const string expected = "Message";
            var target = new ParcelModel() { Message = expected };
            var actual = target.Message;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Parcel
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void ParcelTest()
        {
            var target = new ParcelModel();
            var actual = target.Parcel;
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Receiver
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void ReceiverTest()
        {
            string expected = "You";
            var target = new ParcelModel() { Receiver = expected };
            var actual = target.Receiver;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ReceiverAddress
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void ReceiverAddressTest()
        {
            const string expected = "Your house";
            var target = new ParcelModel() { ReceiverAddress = expected };
            var actual = target.ReceiverAddress;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Sender
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void SenderTest()
        {
            const string expected = "Me";
            var target = new ParcelModel() { Sender = expected };
            var actual = target.Sender;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SenderAddress
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod]
        public void SenderAddressTest()
        {
            string expected = "My house";
            var target = new ParcelModel() { SenderAddress = expected };
            var actual = target.SenderAddress;
            Assert.AreEqual(expected, actual);
        }
    }
}
