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
        private string longUrl;
        private string shortUrl;
        private string shortUrlCode;
        private string invalidMessage;

        [TestInitialize]
        public void Init()
        {
            longUrl = "www.raihantaher.com";
            shortUrl = "kidurl.my/123";
            shortUrlCode = "123";
            invalidMessage = "Invalid URL provided!";
            _mock = new Mock<IKidUrlDataAccess>();
            _target = new KidUrlManager(_mock.Object);
        }

        [TestMethod]
        public void ConvertUrl_LongUrlProvided_ShortUrlReturned()
        {
            // Arrange
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(longUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_InvalidUrl_ReturnMessage()
        {
            // Arrange
            var invalidUrl = "raihantaher/blog/123";
            var invalidMessage = "Invalid URL provided!";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(invalidMessage, result);

        }

        [TestMethod]
        public void ConvertUrl_InvalidUrlNumber2_ReturnMessage()
        {
            // Arrange
            var invalidUrl = "select * from something";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(invalidMessage, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidUrlWithHttp_ReturnMessage()
        {
            // Arrange
            var invalidUrl = @"http://www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidUrlWithHttps_ReturnMessage()
        {
            // Arrange
            var invalidUrl = @"https://www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidUrlWithoutHttpOrHttps_ReturnMessage()
        {
            // Arrange
            var invalidUrl = @"www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidUrlWithoutWWW_ReturnMessage()
        {
            // Arrange
            var invalidUrl = @"raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_InValidUrlWithoutHttpp_ReturnMessage()
        {
            // Arrange
            var invalidUrl = @"httpp://www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(invalidMessage, result);

        }

        [TestMethod]
        public void ConvertUrl_InvalidShortUrl_ReturnMessage()
        {
            // Arrange
            var invalidUrl = @"kidurl.my/raihan";
            _mock.Setup(x => x.GetLongUrl(It.IsAny<string>())).Returns(longUrl);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(invalidMessage, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidShortUrl_ReturnURL()
        {
            // Arrange
            var invalidUrl = @"kidurl.my/d9070a6d-ee73-4f39-be10-64c3711af4ce";
            _mock.Setup(x => x.GetLongUrl(It.IsAny<string>())).Returns(longUrl);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(longUrl, result);

        }
    }
}
