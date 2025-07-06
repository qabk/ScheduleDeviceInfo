using Microsoft.AspNetCore.Http;
using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Interfaces;
using System;
using System.Globalization;

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

        Task<IEnumerable<RawInfoDevice>> IRawInfoDeviceService.getRawInfoDeviceByIdAndStartEndTime(string id, string startTime, string endTime)
        {
            DateTime start = parsingDateTime(startTime);
            DateTime end = parsingDateTime(endTime);
            return _rawInfoDeviceRepository.GetByTimeRangeAndDeviceId(id, start, end);
        }
        private DateTime parsingDateTime(string time)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            DateTime localTime =  DateTime.ParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            DateTimeOffset offsetTime = new DateTimeOffset(localTime, TimeSpan.FromHours(7));
            return offsetTime.UtcDateTime;
        }
    }
}
