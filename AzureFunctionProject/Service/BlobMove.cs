using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using AzureFunctionProject.IService;
using Microsoft.Extensions.Logging;

namespace AzureFunctionProject.Service
{
    public class BlobMove : IBlobMove
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobMove(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public bool Move(string blobName)
        {
            if (blobName == null || blobName == "")
            {
                return false;
            }
            else
            {
                var sourceBlobContainer=_blobServiceClient.GetBlobContainerClient("sourcecontainer");
                var destinationBlobContainer = _blobServiceClient.GetBlobContainerClient("destcontainer");
                var blobClient = sourceBlobContainer.GetBlobClient(blobName);
                var destBlobClient = destinationBlobContainer.GetBlobClient(blobName);
                var sourceBlobSasToken = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddHours(2));
                destBlobClient.StartCopyFromUri(sourceBlobSasToken);
                Console.WriteLine("Copied successfully to destination container");
                var destProps = destBlobClient.GetProperties().Value;
                blobClient.DeleteIfExists();
                return true;
            }
        }
    }
}