using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace WebApiProject.IntegrationTest
{
    public class MailSendIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public MailSendIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"rutuja:test"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        public static class ContentHelper
        {
            public static StringContent GetStringContent(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsEmailSent()
        {
            var request = new
            {
                Url = "api/v1/Mail/SendMail",
                Body = new
                {
                    ToEmail = "rutuborkar5334@gmail.com",
                    Subject = "testing",
                    Body = "test"
                }
            };
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsErrorForEmpty()
        {
            var request = new
            {
                Url = "api/v1/Mail/SendMail",
                Body = new
                {
                    ToEmail = "",
                    Subject = "testing",
                    Body = "test"
                }
            };
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("The ToEmail field is required.", value);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsErrorForNull()
        {
            var request = new
            {
                Url = "api/v1/Mail/SendMail",
                Body = new
                {
                    Subject = "testing",
                    Body = "test"
                }
            };
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("The ToEmail field is required.", value);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsErrorForInvalid()
        {
            var request = new
            {
                Url = "api/v1/Mail/SendMail",
                Body = new
                {
                    ToEmail = "rutuborkar5334",
                    Subject = "testing",
                    Body = "test"
                }
            };
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("The ToEmail field is not a valid e-mail address.", value);
        }
    }
}