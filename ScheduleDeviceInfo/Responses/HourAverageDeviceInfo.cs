using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleDeviceInfo.Responses
{
    public class HourAverageDeviceInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("device_id")]
        public string DeviceId { get; set; }
        [BsonElement("power")]
        public double Power { get; set; }
        [BsonElement("start_time")]
        public DateTime StartTime { get; set; }
        [BsonElement("end_time")]
        public DateTime EndTime { get; set; }



    }
}
