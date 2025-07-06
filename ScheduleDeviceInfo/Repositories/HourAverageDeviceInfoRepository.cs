using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Interfaces;
using ScheduleDeviceInfo.Responses;
using ScheduleDeviceInfo.Settings;

namespace ScheduleDeviceInfo.Repositories
{
    public class HourAverageDeviceInfoRepository : Repository<HourAverageDeviceInfo>, IHourAverageDeviceInfoRepository
    {
        public HourAverageDeviceInfoRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }

        async Task<IEnumerable<HourAverageDeviceInfo>> IHourAverageDeviceInfoRepository.GetByDeviceIdAsync(string deviceId)
        {
            return await _collection.Find(deviceInfo => deviceInfo.DeviceId == deviceId).ToListAsync();
        }
    }
}
