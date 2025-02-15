using Ability_Drive_API.DTOs;
using Ability_Drive_API.Repositories.Ride_Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ability_Drive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideController : ControllerBase
    {
        private readonly IRideRepository _rideRepository;

        public RideController(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }

        [HttpPost("private/{userId}/driver/{driverId}")]
        public async Task<IActionResult> CreatePrivateRide(int userId, int driverId, [FromBody] RideRequestDTO dto, [FromQuery] string voucherCode = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var (isSuccess, message, ride) = await _rideRepository.CreateRideAsync(userId, driverId, dto, voucherCode);

            if (isSuccess)
            {
                return Ok(new { status = true, message = message, ride = ride });
            }
            else
            {
                return Ok(new { status = false, message = message });
            }
        }


        [HttpPatch("bus/{userId}/{busScheduleId}")]
        public async Task<IActionResult> BookBusSeat(int userId, int busScheduleId)
        {
            var seatBookingDTO = await _rideRepository.BookBusSeatAsync(userId, busScheduleId);

            if (seatBookingDTO == null)
            {
                return Ok(new { status = false, message = "No available seats for the selected bus." });
            }

            return Ok(new { status = true, message = "Bus seat booked successfully", seatBooking = seatBookingDTO });
        }

        [HttpGet("bus-schedules")]
        public async Task<IActionResult> GetBusSchedules()
        {
            var schedules = await _rideRepository.GetBusSchedulesAsync();
            return Ok(new { status = true, message = "Bus schedules retrieved successfully", schedules });
        }

        [HttpGet("available-rides")]
        public async Task<IActionResult> GetAvailableRides([FromQuery] int driverId)
        {
            var rides = await _rideRepository.GetPendingRidesByDriverIdAsync(driverId);
            return Ok(new { status = true, message = "Available rides retrieved successfully", rides });
        }
        [HttpGet("{rideId}/check-status")]
        public async Task<IActionResult> GetRideStatus(int rideId)
        {
            try
            {
                var ride = await _rideRepository.GetRideStatusAsync(rideId);
                return Ok(new { status = true, message = "Ride status retrieved successfully", ride });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { status = false, message = "Ride not found." });
            }
        }

        [HttpPut("{rideId}/status")]
        public async Task<IActionResult> UpdateRideStatus(int rideId, [FromBody] RideStatusUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var ride = await _rideRepository.UpdateRideStatusAsync(rideId, dto.Status,dto.Reason);
            if (ride == null)
                return NotFound(new { status = false, message = "Ride not found." });

            return Ok(new { status = true, message = "Ride status updated successfully", ride });
        }
    }
}
