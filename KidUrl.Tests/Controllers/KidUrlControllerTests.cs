using KidUrl.Controllers;
using KidUrl.Manager.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KidUrl.Tests.Controllers
{
    [TestClass]
    public class KidUrlControllerTests
    {
        private Mock<IKidUrlManager> _mock;
        private KidUrlController _target;

        [TestInitialize]
        public void Init()
        {
            _mock = new Mock<IKidUrlManager>();
            _target = new KidUrlController(_mock.Object);
        }

        [TestMethod]
        public void ConvertUrl_LongUrlProvided_ShortUrlRetured_StatusOK()
        {
            // Arrange
            var longUrl = "www.raihantaher.com";
            var shortUrl = "kidurl.my/123";
            _mock.Setup(x => x.ConvertUrl(It.IsAny<string>())).Returns(shortUrl);

            // Act
            var result = _target.ConvertUrl(longUrl).Result as OkObjectResult;

            // Assert
            Assert.AreEqual(shortUrl, result.Value);
        }

        [TestMethod]
        public void ConvertUrl_InvalidLongUrlProvided_StatusBadRequest()
        {
            // Arrange
            var longUrl = "wwwraihantahercom";
            var invalidMessage = "Invalid URL provided!";
            _mock.Setup(x => x.ConvertUrl(It.IsAny<string>())).Returns(invalidMessage);

            // Act
            var result = _target.ConvertUrl(longUrl).Result as BadRequestResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void ConvertUrl_ShortUrlNotInDatabase_StatusNoContent()
        {
            // Arrange
            var longUrl = "wwwraihantahercom";
            _mock.Setup(x => x.ConvertUrl(It.IsAny<string>())).Returns((string)null);

            // Act
            var result = _target.ConvertUrl(longUrl).Result as NoContentResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void ConvertUrl_ShortUrlEmpty_StatusNotFound()
        {
            // Arrange
            var longUrl = "wwwraihantahercom";
            _mock.Setup(x => x.ConvertUrl(It.IsAny<string>())).Returns("");

            // Act
            var result = _target.ConvertUrl(longUrl).Result as NotFoundResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
