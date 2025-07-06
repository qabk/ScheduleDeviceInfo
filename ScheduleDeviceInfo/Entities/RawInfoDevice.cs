using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ScheduleDeviceInfo.Entities
{
    public class RawInfoDevice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        [BsonElement("device_id")]
        public string DeviceId { get; set; } 

        [BsonElement("current")]
        public double Current { get; set; } 

        [BsonElement("i_peakmax")]
        public double IPeakmax { get; set; } 

        [BsonElement("i_peakmax_10s")]
        public double IPeakmax10s { get; set; } 

        [BsonElement("voltage")]
        public double Voltage { get; set; } 

        [BsonElement("u_peakmax")]
        public double UPeakmax { get; set; } 

        [BsonElement("u_peakmax_10s")]
        public double UPeakmax10s { get; set; } 

        [BsonElement("cos_phi")]
        public double Cosphi { get; set; } 

        [BsonElement("power")]
        public double Power { get; set; } 

        [BsonElement("received_time")]
        public DateTime ReceivedTime { get; set; } 
    }
}
