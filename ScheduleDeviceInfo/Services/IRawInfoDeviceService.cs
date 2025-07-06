using ScheduleDeviceInfo.Entities;

namespace ScheduleDeviceInfo.Services
{
    public interface IRawInfoDeviceService
    {
        public Task<IEnumerable<RawInfoDevice>> getRawInfoDeviceById(string id);
        public Task createRawInfoDevice(RawInfoDevice info);
        public Task createListOfRawInfoDevice(IEnumerable<RawInfoDevice> info);
    }
}
