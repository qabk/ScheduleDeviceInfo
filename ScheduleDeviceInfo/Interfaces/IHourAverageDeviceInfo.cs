using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Responses;

namespace ScheduleDeviceInfo.Interfaces
{
    public interface IHourAverageDeviceInfoRepository
    {
        Task<IEnumerable<HourAverageDeviceInfo>> GetByDeviceIdAsync(string deviceId);
    }
}
