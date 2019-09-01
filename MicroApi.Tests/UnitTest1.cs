using System.Threading.Tasks;
using MicroApi.Handlers;
using MicroApi.Models;
using MicroApi.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace MicroApi.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public async Task Test1()
        {
            var mockAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            mockAccessor.Setup(accessor => accessor.HttpContext).Returns(context);
            
            var mockForecast = new Mock<IForecastService>();
            mockForecast.Setup(x=> x.GetById(It.IsAny<int>())).Returns(new WeatherForecast()
            {
                Summary = "TEST"
            });
            
            ForecastHandler handler = new ForecastHandler(mockForecast.Object, mockAccessor.Object);
            await handler.GetForecastById();
            
            Assert.AreEqual(200, context.Response.StatusCode);
        }
    }
}
