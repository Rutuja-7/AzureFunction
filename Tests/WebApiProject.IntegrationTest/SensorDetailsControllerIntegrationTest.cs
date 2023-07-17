using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiProject.IntegrationTest
{
    public class SensorDetailsControllerIntegrationTest : IClassFixture<WebAppFactoryTest<Program>>
    {
        private readonly HttpClient _httpClient;
        public SensorDetailsControllerIntegrationTest(WebAppFactoryTest<Program> factory)
        {
            _httpClient = factory.CreateClient();
            
        }

        public static class ContentHelper
        {
            public static StringContent GetStringContent(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsSensorCreated()
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"rutuja:test"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            var request = new
            {
                Url = "api/v1/SensorDetailsNew/AddSensor",
                Body = new
                {
                    SensorType = "test",
                    Pressure = "10Pa",
                    Temperature = "20",
                    SupplyVoltageLevel = "20V",
                    Accuracy = "15%"
                }
            };
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Contains("test", value);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsError()
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"rutuja:test"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            var request = new
            {
                Url = "api/v1/SensorDetailsNew/AddSensor",
                Body = new
                {
                    SensorType = "test",
                    Pressure = "10",
                    Temperature = "20",
                    SupplyVoltageLevel = "20V",
                    Accuracy = "15%"
                }
            };
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsInvalidCredentials()
        {
            var cerdentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"rutuja:testcore"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cerdentials);
            var request = new
            {
                Url = "api/v1/SensorDetailsNew/AddSensor",
                Body = new
                {
                    SensorType = "test",
                    Pressure = "10Pa",
                    Temperature = "20",
                    SupplyVoltageLevel = "20V",
                    Accuracy = "15%"
                }
            };
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsInvalidAuthorizationHeader()
        {
            var cerdentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($""));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cerdentials);
            var request = new
            {
                Url = "api/v1/SensorDetailsNew/AddSensor",
                Body = new
                {
                    SensorType = "test",
                    Pressure = "10Pa",
                    Temperature = "20",
                    SupplyVoltageLevel = "20V",
                    Accuracy = "15%"
                }
            };
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsMissingAuthorizationHeader()
        {
            var request = new
            {
                Url = "api/v1/SensorDetailsNew/AddSensor",
                Body = new
                {
                    SensorType = "test",
                    Pressure = "10Pa",
                    Temperature = "20",
                    SupplyVoltageLevel = "20V",
                    Accuracy = "15%"
                }
            };
            var response = await _httpClient.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}