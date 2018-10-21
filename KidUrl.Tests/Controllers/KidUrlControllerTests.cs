using KidUrl.Controllers;
using KidUrl.Manager.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.Mvc;

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
    }
}
