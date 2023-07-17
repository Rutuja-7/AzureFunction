using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureFunctionProject;
using AzureFunctionProject.IService;
using Moq;
using Moq.Protected;

namespace AzureFunctionProjectTest.Systems.MailSend
{
    public class MailSendTest
    {
        [Fact]
        public void SendMail_Success()
        {
            var dataSource = new Mock<IBlobMove>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
               new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK
               }).Verifiable();
            var httpClient = new HttpClient(handlerMock.Object);
            var conversion = new JsonTosql(dataSource.Object, httpClient);
            var result = conversion.SendMail(100);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public void SendMail_Failure()
        {
            var dataSource = new Mock<IBlobMove>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
               new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.NotFound
               }).Verifiable();
            var httpClient = new HttpClient(handlerMock.Object);
            var conversion = new JsonTosql(dataSource.Object, httpClient);
            var result = conversion.SendMail(100);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public void SendMail_SuccessTrue()
        {
            var dataSource = new Mock<IBlobMove>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
               new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK
               }).Verifiable();
            var httpClient = new HttpClient(handlerMock.Object);
            var conversion = new JsonTosql(dataSource.Object, httpClient);
            bool isValid = conversion.SendMail(100).Result;
            Assert.True(isValid);
        }

        [Fact]
        public void SendMail_SuccessFalse()
        {
            var dataSource = new Mock<IBlobMove>();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
               new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK
               }).Verifiable();
            var httpClient = new HttpClient(handlerMock.Object);
            var conversion = new JsonTosql(dataSource.Object, httpClient);
            bool isValid = conversion.SendMail(10).Result;
            Assert.False(isValid);
        }
    }
}