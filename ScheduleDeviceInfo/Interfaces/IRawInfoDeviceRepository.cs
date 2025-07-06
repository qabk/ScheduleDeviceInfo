using ScheduleDeviceInfo.Entities;

namespace ScheduleDeviceInfo.Interfaces
{
    public interface IRawInfoDeviceRepository : IRepository<RawInfoDevice>
    {
        Task<IEnumerable<RawInfoDevice>> GetByDeviceIdAsync(string deviceId);
        Task<IEnumerable<RawInfoDevice>> GetByTimeRangeAndDeviceId(string deviceId, DateTime start, DateTime end);
    }
}
