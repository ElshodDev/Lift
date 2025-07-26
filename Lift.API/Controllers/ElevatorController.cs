using Lift.API.Models;
using Lift.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lift.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElevatorController : ControllerBase
    {
        private readonly ElevatorService _elevatorService;

        public ElevatorController(ElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _elevatorService.GetCurrentStatusAsync();
            if (status == null)
                return NotFound(new { message = "Lift statusi topilmadi" });

            return Ok(status);
        }

        [HttpPost("request")]
        public async Task<IActionResult> SendRequest([FromBody] ElevatorRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "So‘rov noto‘g‘ri: ma'lumot yuborilmadi." });
            }
            if (request.RequestedFloor < 1 || request.RequestedFloor > 10)
            {
                return BadRequest(new { message = "Xato: Qavat 1 dan 10 gacha bo‘lishi kerak." });
            }

            await _elevatorService.AddRequestAsync(request.RequestedFloor);
            return Ok(new { message = "So'rov qabul qilindi" });
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetRequests()
        {
            var requests = await _elevatorService.GetAllRequestsAsync();
            return Ok(requests);
        }
    }
}
