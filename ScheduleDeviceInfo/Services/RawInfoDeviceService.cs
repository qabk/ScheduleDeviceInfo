using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Interfaces;

namespace ScheduleDeviceInfo.Services
{
    public class RawInfoDeviceService: IRawInfoDeviceService
    {
        private readonly IRawInfoDeviceRepository _rawInfoDeviceRepository;
        public RawInfoDeviceService(IRawInfoDeviceRepository rawInfoDeviceRepository)
        {
            _rawInfoDeviceRepository = rawInfoDeviceRepository;
        }

        public Task<IEnumerable<RawInfoDevice>> getRawInfoDeviceById(string id) {
            return _rawInfoDeviceRepository.GetByDeviceIdAsync(id);
        }

        Task IRawInfoDeviceService.createListOfRawInfoDevice(IEnumerable<RawInfoDevice> info) => _rawInfoDeviceRepository.CreateListAsync(info);

        Task IRawInfoDeviceService.createRawInfoDevice(RawInfoDevice info)
        {
            return _rawInfoDeviceRepository.CreateAsync(info);
        }
    }
}
