using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureFunctionProject.IService;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApiProject.Models;

namespace AzureFunctionProject
{
    public class JsonTosql
    {
        private readonly IBlobMove _blobMove;
        private readonly HttpClient _httpClient;
        public const string BlobContainer = "sourcecontainer";
        public JsonTosql(IBlobMove blobMove, HttpClient httpClient)
        {
            _blobMove = blobMove;
            _httpClient = httpClient;
        }

        [ExcludeFromCodeCoverage]
        [FunctionName(nameof(JsonTosql))]
        public async Task Run([BlobTrigger(BlobContainer + "/{name}.json", Connection = "AzureWebJobsStorage")] Stream sensorBlob, string name, ILogger log)
        {
            var blobName = $"{name}.json";
            try
            {
                //coverting from sensorBlob to sensor model
                var sensor = new SensorDetails();
                using (var reader = new StreamReader(sensorBlob))
                {
                    var input = reader.ReadToEnd();
                    sensor = JsonConvert.DeserializeObject<SensorDetails>(input);
                }

                if (sensor != null)
                {
                    var temperature = Convert.ToInt32(sensor.Temperature);
                    //Checking temperature value. Temperature value goes above then sending mail notification to specified user
                    if (temperature > 90)
                    {
                        var result = await SendMail(temperature);
                        log.LogInformation($"mail sending result : {result}");
                    }
                    else
                    {
                        var result = await AddSensorRecord(sensor, blobName);
                        log.LogInformation($"result : {result}");
                    }
                }
            }
            catch (Exception e)
            {
                //Log Exception
                log.LogError($"Error: {e.Message}");
            }
        }

        public async Task<bool> SendMail(int temperature)
        {
            if (temperature > 90)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7020/api/v1/Mail/SendMail");
                var postData = new MailRequest()
                {
                    ToEmail = "rutuborkar5334@gmail.com",
                    Subject = "Temperature exceeded",
                    Body = "Hi User, Temperature value is greater than 90°C."
                };
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("rutuja:test");
                string val = System.Convert.ToBase64String(plainTextBytes);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

                var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress, content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    //log.LogInformation($"result : {response.EnsureSuccessStatusCode()}");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddSensorRecord(SensorDetails sensor, string blobName)
        {
            if (blobName != null && blobName != "" && sensor.SensorType != "")
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7020/api/v1/SensorDetailsNew/AddSensor");
                var postData = new
                {
                    SensorType = sensor.SensorType,
                    Pressure = sensor.Pressure,
                    Temperature = sensor.Temperature,
                    SupplyVoltageLevel = sensor.SupplyVoltageLevel,
                    Accuracy = sensor.Accuracy
                };
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("rutuja:test");
                string val = System.Convert.ToBase64String(plainTextBytes);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress, content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    //Once data got saved to database moving the file to destination container and deleting it from source container 
                    if (Convert.ToInt32(response.StatusCode) == 200)
                    {
                        var moveResult = _blobMove.Move(blobName);
                    }
                }
                return true;
            }
            return false;
        }
    }
}