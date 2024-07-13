using FluentAssertions;
using FormulaApp.Api.Controllers;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services.Interfaces;
using FormulaApp.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FormulaApp.UnitTests.Systems.Controllers
{
    public class TestFansController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(FansFixtures.GetFans());
            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (OkObjectResult)await fansController.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokeService()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(FansFixtures.GetFans());
            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (OkObjectResult) await fansController.Get();

            //Assert
            mockFanService.Verify(service => service.GetAllFans(), Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnListOfFans()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(FansFixtures.GetFans());
            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (OkObjectResult) await fansController.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<List<Fan>>();
        }

        [Fact]
        public async Task Get_OnNoFans_ReturnNotFound()
        {
            //Arrange
            var mockService = new Mock<IFanService>();
            mockService.Setup(service => service.GetAllFans())
                .ReturnsAsync(new List<Fan>());
            var fansController = new FansController(mockService.Object);

            //Act
            var result = (NotFoundResult) await fansController.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
