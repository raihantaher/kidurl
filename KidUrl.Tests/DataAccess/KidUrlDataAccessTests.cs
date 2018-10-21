using KidUrl.DataAccess;
using KidUrl.DataAccess.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KidUrl.Tests.DataAccess
{
    [TestClass]
    public class KidUrlDataAccessTests
    {
        private Mock<IKidUrlDataAccess> _mock;
        private KidUrlDataAcess _target;

        [TestInitialize]
        public void Init()
        {
            _mock = new Mock<IKidUrlDataAccess>();
            _target = new KidUrlDataAcess();
        }

        [TestMethod]
        public void GetLongUrl_LongUrl_ShortUrlReturn()
        {
            // Arrange
            var shortCode = @"d9070a6d-ee73-4f39-be10-64c3711af4ce";
            var longurl = @"http://www.raihantaher.com";

            // Act
            var holder = _target.GetLongUrl(shortCode);

            // Assert
            Assert.AreEqual(longurl, holder);
            
        }

        [TestMethod]
        public void GetLongUrl_ShortUrl_LongUrlReturn()
        {
            // Arrange
            var shortCode = @"5de65d87-f7c9-4260-8b43-2753c2a3e5f4";
            var longurl = @"www.google.com";

            // Act
            var holder = _target.GetShortUrl(longurl);

            // Assert
            Assert.AreEqual(shortCode, holder);

        }

        //[TestMethod]
        //public void GetLongUrl_ShortUrl_LongUrlSave()
        //{
        //    // Arrange
        //    var longurl = @"http://www.hello.com";

        //    // Act
        //    var holder = _target.GetShortUrl(longurl);

        //    // Assert
        //    Assert.IsNotNull(holder);

        //}
    }
}
