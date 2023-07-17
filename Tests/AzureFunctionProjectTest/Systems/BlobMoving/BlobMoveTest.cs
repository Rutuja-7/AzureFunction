using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using AzureFunctionProject.Service;
using Moq;

namespace AzureFunctionProjectTest.Systems.BlobMoving
{
    public class BlobMoveTest
    {
        [Fact]
        public async Task BlobMove_Success()
        {
            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockContainerClient = new Mock<BlobContainerClient>();
            var mockBlobClient = new Mock<BlobClient>();
            mockBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(mockContainerClient.Object);
            mockContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(mockBlobClient.Object);
            var properties = new BlobProperties { };
            mockBlobClient.Setup(x => x.GetProperties(null, default)).Returns(Response.FromValue(properties, null));
            mockBlobClient.Setup(x => x.GenerateSasUri(It.IsAny<BlobSasPermissions>(), It.IsAny<DateTimeOffset>())).Returns(new Uri("http://mocked-sas-uri"));

            var mock = new BlobMove(mockBlobServiceClient.Object);
            var result = mock.Move("Sensor65.json");
            Assert.True(result);
        }

        [Fact]
        public async Task BlobMove_Fail_BlobNameEmpty()
        {
            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockContainerClient = new Mock<BlobContainerClient>();
            var mockBlobClient = new Mock<BlobClient>();
            mockBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(mockContainerClient.Object);
            mockContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(mockBlobClient.Object);
            var properties = new BlobProperties { };
            mockBlobClient.Setup(x => x.GetProperties(null, default)).Returns(Response.FromValue(properties, null));
            mockBlobClient.Setup(x => x.GenerateSasUri(It.IsAny<BlobSasPermissions>(), It.IsAny<DateTimeOffset>())).Returns(new Uri("http://mocked-sas-uri"));

            var mock = new BlobMove(mockBlobServiceClient.Object);
            var result = mock.Move("");
            Assert.False(result);
        }

        [Fact]
        public async Task BlobMove_Fail_BlobNameNull()
        {
            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockContainerClient = new Mock<BlobContainerClient>();
            var mockBlobClient = new Mock<BlobClient>();
            mockBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(mockContainerClient.Object);
            mockContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(mockBlobClient.Object);
            var properties = new BlobProperties { };
            mockBlobClient.Setup(x => x.GetProperties(null, default)).Returns(Response.FromValue(properties, null));
            mockBlobClient.Setup(x => x.GenerateSasUri(It.IsAny<BlobSasPermissions>(), It.IsAny<DateTimeOffset>())).Returns(new Uri("http://mocked-sas-uri"));

            var mock = new BlobMove(mockBlobServiceClient.Object);
            var result = mock.Move(null);
            Assert.False(result);
        }

        [Fact]
        public async Task BlobMove_Success_DeleteCalledOnce()
        {
            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockContainerClient = new Mock<BlobContainerClient>();
            var mockBlobClient = new Mock<BlobClient>();
            mockBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(mockContainerClient.Object);
            mockContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>())).Returns(mockBlobClient.Object);
            mockBlobClient.Setup(x => x.GenerateSasUri(It.IsAny<BlobSasPermissions>(), It.IsAny<DateTimeOffset>())).Returns(new Uri("http://127.0.0.1:10000/devstoreaccount1/blobcontainertest"));
            var properties = new BlobProperties();
            mockBlobClient.Setup(x => x.GetProperties(null, default)).Returns(Response.FromValue(properties, null));

            var mock = new BlobMove(mockBlobServiceClient.Object);
            var result = mock.Move("Sensor65.json");
            mockBlobClient.Verify(x => x.DeleteIfExists(default, null, default), Times.Exactly(1));
        }
    }
}