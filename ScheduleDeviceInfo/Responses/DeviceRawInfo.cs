using System.Text.Json.Serialization;

namespace ScheduleDeviceInfo.Responses
{
    public class Root
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("result")]
        public List<ResultItem> Result { get; set; }
    }

    public class ResultItem
    {
        public string Id { get; set; }
        public List<RawInfoDeviceJson> Data { get; set; }
    }

    public class RawInfoDeviceJson
    {
        public double Current { get; set; }
        public double Ipeakmax { get; set; }
        public double Ipeakmax10s { get; set; }
        public double Voltage { get; set; }
        public double Upeakmax { get; set; }
        public double Upeakmax10s { get; set; }
        public double Cosphi { get; set; }
        public double Power { get; set; }
        public string ReceivedTime { get; set; }
    }
}
