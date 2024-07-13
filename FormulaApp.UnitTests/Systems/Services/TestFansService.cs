using FluentAssertions;
using FormulaApp.Api.Configuration;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services;
using FormulaApp.UnitTests.Fixtures;
using FormulaApp.UnitTests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaApp.UnitTests.Systems.Services
{
    public class TestFansService
    {
        [Fact]
        public async Task GetAllFans_OnInvoked_HttpGet()
        {
            //Arrange
            var url = "https://mywebsite.com/api/v1/fans";

            var response = FansFixtures.GetFans();
            var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
            var httpClient = new HttpClient(mockHandler.Object);

            var config = Options.Create(new ApiServiceConfig { Url = url });
            var fanService = new FanService(httpClient, config);

            //Act
            await fanService.GetAllFans();

            //Assert
            mockHandler.Protected().Verify(
                "SendAsync", Times.Once(),
                ItExpr.Is<HttpRequestMessage>(r=> r.Method == HttpMethod.Get && r.RequestUri.ToString() == url),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAllFans_OnInvoked_ListOfFans()
        {
            //Arrange
            var url = "https://mywebsite.com/api/v1/fans";

            var response = FansFixtures.GetFans();
            var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
            var httpClient = new HttpClient(mockHandler.Object);

            var config = Options.Create(new ApiServiceConfig { Url = url });
            var fanService = new FanService(httpClient, config);

            //Act
            var results = await fanService.GetAllFans();

            //Assert
            results.Should().BeOfType<List<Fan>>();
        }

        [Fact]
        public async Task GetAllFans_OnInvoked_ReturnEmptyList()
        {
            //Arrange
            var url = "https://mywebsite.com/api/v1/fans";

            var response = FansFixtures.GetFans();
            var mockHandler = MockHttpHandler<Fan>.SetUpReturnNotFound(response);
            var httpClient = new HttpClient(mockHandler.Object);

            var config = Options.Create(new ApiServiceConfig { Url = url });
            var fanService = new FanService(httpClient, config);

            //Act
            var results = await fanService.GetAllFans();

            //Assert
            results.Count.Should().Be(0);
        }
    }
}
