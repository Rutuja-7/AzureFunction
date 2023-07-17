using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureFunctionProject;
using AzureFunctionProject.IService;
using AzureFunctionProjectTest.MockData;
using Moq;
using Moq.Protected;

namespace AzureFunctionProjectTest.Systems.AddSensorRecord
{
    public class AddSensorRecordTest
    {
        [Fact]
        public void AddRecordAndBlobMove_Success()
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
            var sensor = MockDataTest.AddSensor();
            var result = conversion.AddSensorRecord(sensor, "Sensor63.json");
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void AddRecordAndBlobMove_SuccessTrue()
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
            var sensor = MockDataTest.AddSensor();
            var result = conversion.AddSensorRecord(sensor, "Sensor63.json").Result;
            Assert.True(result);
        }

        [Fact]
        public void AddRecordAndBlobMove_SuccessFalse()
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
            var sensor = MockDataTest.AddSensorBadRecord();
            var result = conversion.AddSensorRecord(sensor, "Sensor63.json").Result;
            Assert.False(result);
        }

        [Fact]
        public void AddRecordAndBlobMove_blobNameEmpty_SuccessFalse()
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
            var sensor = MockDataTest.AddSensorBadRecord();
            var result = conversion.AddSensorRecord(sensor, "").Result;
            Assert.False(result);
        }
    }
}