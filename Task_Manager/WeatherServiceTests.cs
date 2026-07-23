using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_Manager;
using Moq;
using System.Net.Http;
using Moq.Protected;
using System.Net;
using System.Text.Json;

using Task__Manager;

namespace Task_Manager
{
    [TestClass]
    public class WeatherServiceTests
    {
        [TestMethod]
        public async Task GetCurrentWeatherAsync_ValidResponse_ReturnsProperlyDeserializedObject()
        {
            // Arrange
            string simulatedJsonResponse = @"{
                ""name"": ""Cape Town"",
                ""main"": { ""temp"": 22.5 },
                ""weather"": [{ ""description"": ""clear sky"" }]
            }";

            // Mock the HttpMessageHandler (SendAsync is protected, so we use Moq.Protected)
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(simulatedJsonResponse)
                });

            // Inject the mocked handler into an HttpClient
            var httpClient = new HttpClient(handlerMock.Object);

            // Instantiate the WeatherService with the fake client
            var weatherService = new WeatherService(httpClient, "fake_api_key");

            // Act
            var result = await weatherService.GetCurrentWeatherAsync("Cape Town");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Cape Town", result.Name);
            Assert.IsNotNull(result.Main);
            Assert.AreEqual(22.5, result.Main.Temp);
            Assert.IsNotNull(result.Weather);
            Assert.AreEqual("clear sky", result.Weather[0].Description);
        }
        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetCurrentWeatherAsync_ApiError_ThrowsHttpRequestException()
        {
            //Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("{\"cod\":401, \"message\": \"Invalid API key\"}")
                });
            var httpClient = new HttpClient(handlerMock.Object);
            var weatherService = new WeatherService(httpClient, "fake_api_key");
            //Act
            // This should throw an HttpRequestException because of response.EnsureSuccessStatusCode();
            await weatherService.GetCurrentWeatherAsync("Cape Town");
        }
        [TestMethod]
        public async Task GetForecastAsync_ValidResponse_ReturnsProperlyDeserializedObject()
        {
            // Arrange
            string simulatedJsonResponse = @"{
        ""city"": { ""name"": ""Cape Town"" },
        ""list"": [
            {
                ""dt_txt"": ""2026-07-22 15:00:00"",
                ""main"": { ""temp"": 15.74 },
                ""weather"": [{ ""description"": ""scattered clouds"" }]
            }
        ]
    }";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(simulatedJsonResponse, System.Text.Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var weatherService = new WeatherService(httpClient, "fake_api_key");

            // Act
            var result = await weatherService.GetForecastAsync("Cape Town");

            // Assert
            Assert.IsNotNull(result, "ForecastResponse should not be null.");
            Assert.IsNotNull(result.City, "City property should not be null.");
            Assert.AreEqual("Cape Town", result.City.Name, "City name did not match.");
            Assert.IsNotNull(result.List, "Forecast list should not be null.");
            Assert.IsTrue(result.List.Length > 0, "Forecast list should contain items.");
            Assert.AreEqual("2026-07-22 15:00:00", result.List[0].Dt_txt);
            Assert.AreEqual(15.74, result.List[0].Main.Temp);
            Assert.AreEqual("scattered clouds", result.List[0].Weather[0].Description);
        }
    }
}
