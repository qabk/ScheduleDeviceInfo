using MongoDB.Bson;
using Newtonsoft.Json;
using Quartz;
using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Responses;
using ScheduleDeviceInfo.Services;
using System.Globalization;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ScheduleDeviceInfo.Schedulers
{
    public class GetDeviceInfoJob : IJob
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiUrl = "http://113.160.87.222:5000/api/data";
        private readonly IRawInfoDeviceService _rawInfoDeviceService;
        public GetDeviceInfoJob(IRawInfoDeviceService rawInfoDeviceService)
        {
            _rawInfoDeviceService = rawInfoDeviceService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                DateTime time = DateTime.Now;
                Console.WriteLine($"\nTime: {convertDateTimeToString(time)}");

                var requestBody = new
                {
                    id = "0000000010",
                    fromDate = convertDateTimeToString(time.AddMinutes(-1)),
                    toDate = convertDateTimeToString(time),
                    field = ""
                };
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

                // Ensure the response was successful
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP status code is an error

                string responseBody = await response.Content.ReadAsStringAsync(); 
                List<RawInfoDevice>  info = ConvertToRawInfoDevices(responseBody);
                await _rawInfoDeviceService.createListOfRawInfoDevice(info);
                Console.WriteLine($"\ntest body: {responseBody}");

            }
            catch (Exception ex)

            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }


            // Read the response content as a string
            //string responseBody = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"Request Body success: ${requestBody}");

        }
        private String convertDateTimeToString(DateTime time) { return time.ToString("yyyy-MM-dd HH:mm:ss"); }
        public List<RawInfoDevice> ConvertToRawInfoDevices(string json)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var root = JsonSerializer.Deserialize<Root>(json, options);


            var rawDevices = new List<RawInfoDevice>();
            if (root.Result.Count == 0) return rawDevices;

            foreach (var resultItem in root.Result)
            {
                foreach (var item in resultItem.Data)
                {
                    rawDevices.Add(new RawInfoDevice
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        DeviceId = resultItem.Id,
                        Current = item.Current,
                        IPeakmax = item.Ipeakmax,
                        IPeakmax10s = item.Ipeakmax10s,
                        Voltage = item.Voltage,
                        UPeakmax = item.Upeakmax,
                        UPeakmax10s = item.Upeakmax10s,
                        Cosphi = item.Cosphi, // scale if needed, or round/truncate
                        Power = item.Power,
                        ReceivedTime = DateTime.ParseExact(item.ReceivedTime != null ? item.ReceivedTime : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                    });
                }
            }

            return rawDevices;
        }
    }
}
