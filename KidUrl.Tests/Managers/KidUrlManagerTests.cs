using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using KidUrl.DataAccess.Interface;
using KidUrl.Manager;

namespace KidUrl.Tests.Managers
{
    [TestClass]
    public class KidUrlManagerTests
    {
        private Mock<IKidUrlDataAccess> _mock;
        private KidUrlManager _target;

        [TestInitialize]
        public void Init()
        {
            _mock = new Mock<IKidUrlDataAccess>();
            _target = new KidUrlManager(_mock.Object);
        }

        [TestMethod]
        public void ConvertUrl_LongUrlProvided_ShortUrlReturned()
        {
            // Arrange
            var longUrl = "www.raihantaher.com";
            var shortUrl = "kidurl.my/123";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns("123");

            // Act
            var result = _target.ConvertUrl(longUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }
    }
}
