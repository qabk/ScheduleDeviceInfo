using Microsoft.AspNetCore.Mvc;
using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Services;

namespace ScheduleDeviceInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RawInfoDeviceController : ControllerBase
    {
        private readonly IRawInfoDeviceService _rawInfoDeviceService;
        public RawInfoDeviceController(IRawInfoDeviceService rawInfoDeviceService) { _rawInfoDeviceService = rawInfoDeviceService; }

         [HttpGet( "GetRawInfoDeviceById/{id}")]
        public async Task<IActionResult> GetRawInfoDeviceById(string id)
        {
            var result = await _rawInfoDeviceService.getRawInfoDeviceById(id);
            return Ok(result);
        }

        [HttpPost("CreateRawInfoDevice")]

        public async Task<IActionResult> CreateRawInfoDevice()
        {
            RawInfoDevice info = new RawInfoDevice();
            await _rawInfoDeviceService.createRawInfoDevice(info);
            return Ok("ok");
        }

    }
}
