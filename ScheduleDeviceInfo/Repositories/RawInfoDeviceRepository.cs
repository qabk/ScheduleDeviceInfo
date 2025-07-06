using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Interfaces;
using ScheduleDeviceInfo.Settings;

namespace ScheduleDeviceInfo.Repositories
{
    public class RawInfoDeviceRepository : Repository<RawInfoDevice>, IRawInfoDeviceRepository
    {
        public RawInfoDeviceRepository(IOptions<MongoDbSettings> settings) : base(settings) { }

        async Task<IEnumerable<RawInfoDevice>> IRawInfoDeviceRepository.GetByDeviceIdAsync(string deviceId)
        {
            return await _collection.Find(deviceInfo => deviceInfo.DeviceId == deviceId).ToListAsync();
        }

        async Task<IEnumerable<RawInfoDevice>> IRawInfoDeviceRepository.GetByTimeRangeAndDeviceId(string deviceId, DateTime start, DateTime end)
        { 
            return await _collection.Find(x => x.DeviceId == deviceId && 
            x.ReceivedTime.CompareTo(start) >= 0 &&
            x.ReceivedTime.CompareTo(end) <=0).ToListAsync();
        }
    }
}
