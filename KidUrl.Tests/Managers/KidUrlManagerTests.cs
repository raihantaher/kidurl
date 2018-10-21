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
        public void ConvertUrl_InvalidUrl_ReturnMessageThatUrlIsInvalid()
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
        public void ConvertUrl_InvalidUrlNumber2_ReturnMessageThatUrlIsInvalid()
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
        public void ConvertUrl_ValidLongUrlWithHttp_ReturnShortUrl()
        {
            // Arrange
            var validLongUrl = @"http://www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(validLongUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidLongUrlWithHttps_ReturnShortUrl()
        {
            // Arrange
            var validLongUrl = @"https://www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(validLongUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidLongUrlWithoutHttpOrHttps_ReturnShortUrl()
        {
            // Arrange
            var validLongUrl = @"www.raihantaher.com";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(validLongUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidLongUrlWithoutWWW_ReturnShortUrl()
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
        public void ConvertUrl_InValidLongUrlWithoutHttpp_ReturnMessageThatUrlIsInvalid()
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
        public void ConvertUrl_InvalidShortUrl_ReturnMessageThatUrlIsInvalid()
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
            var validShortUrl = @"kidurl.my/d9070a6d-ee73-4f39-be10-64c3711af4ce";
            _mock.Setup(x => x.GetLongUrl(It.IsAny<string>())).Returns(longUrl);

            // Act
            var result = _target.ConvertUrl(validShortUrl);

            // Assert
            Assert.AreEqual(longUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidShortUrlWithHttp_ReturnLongURL()
        {
            // Arrange
            var validUrl = @"http://kidurl.my/d9070a6d-ee73-4f39-be10-64c3711af4ce";
            _mock.Setup(x => x.GetLongUrl(It.IsAny<string>())).Returns(longUrl);

            // Act
            var result = _target.ConvertUrl(validUrl);

            // Assert
            Assert.AreEqual(longUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidShortUrlWithHttps_ReturnLongURL()
        {
            // Arrange
            var validUrl = @"https://kidurl.my/d9070a6d-ee73-4f39-be10-64c3711af4ce";
            _mock.Setup(x => x.GetLongUrl(It.IsAny<string>())).Returns(longUrl);

            // Act
            var result = _target.ConvertUrl(validUrl);

            // Assert
            Assert.AreEqual(longUrl, result);

        }

        [TestMethod]
        public void ConvertUrl_ValidLongUrlWithKidUrl_ReturnShortURL()
        {
            // Arrange
            var validUrl = @"https://www.google.my/kidurl/kidurlmy";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(validUrl);

            // Assert
            Assert.AreEqual(shortUrl, result);
        }

        [TestMethod]
        public void ConvertUrl_InvalidLongUrlWithKidUrl_ReturnMessageThatUrlIsInvalid()
        {
            // Arrange
            var invalidUrl = @"www.google.my/kidurl.my/";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(invalidMessage, result);
        }

        [TestMethod]
        public void ConvertUrl_InvalidLongUrlWithKidUrlDash_ReturnMessageThatUrlIsInvalid()
        {
            // Arrange
            var invalidUrl = @"www.google-kidurl.my/";
            _mock.Setup(x => x.GetShortUrl(It.IsAny<string>())).Returns(shortUrlCode);

            // Act
            var result = _target.ConvertUrl(invalidUrl);

            // Assert
            Assert.AreEqual(invalidMessage, result);
        }
    }
}
