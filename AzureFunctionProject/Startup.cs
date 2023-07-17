using System;
using System.Diagnostics.CodeAnalysis;
using Azure.Storage.Blobs;
using AzureFunctionProject.IService;
using AzureFunctionProject.Service;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctionProject.Startup))]
namespace AzureFunctionProject
{
    [ExcludeFromCodeCoverage]
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IBlobMove, BlobMove>();
            builder.Services.AddScoped(_ =>{
                return new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
                });
        }
    }
}