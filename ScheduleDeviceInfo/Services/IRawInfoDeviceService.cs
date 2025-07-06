using ScheduleDeviceInfo.Entities;

namespace ScheduleDeviceInfo.Services
{
    public interface IRawInfoDeviceService
    {
        public Task<IEnumerable<RawInfoDevice>> getRawInfoDeviceById(string id);
        public Task createRawInfoDevice(RawInfoDevice info);
        public Task createListOfRawInfoDevice(IEnumerable<RawInfoDevice> info);
        public Task<IEnumerable<RawInfoDevice>> getRawInfoDeviceByIdAndStartEndTime(string id, string startTime, string endTime);


    }
}
