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

        // ✅ Book a Private Ride
        [HttpPost("private/{userId}")]
        public async Task<IActionResult> CreatePrivateRide(int userId, [FromBody] RideRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var ride = await _rideRepository.CreateRideAsync(userId, dto);

            return Ok(new { status = true, message = "Private ride booked successfully", ride });
        }

        // ✅ Book a Seat in a Bus
        [HttpPost("bus/{userId}/{busScheduleId}")]
        public async Task<IActionResult> BookBusSeat(int userId, int busScheduleId)
        {
            var seatBooking = await _rideRepository.BookBusSeatAsync(userId, busScheduleId);
            if (seatBooking == null)
                return BadRequest(new { status = false, message = "No available seats for the selected bus." });

            return Ok(new { status = true, message = "Bus seat booked successfully", seatBooking });
        }

        [HttpGet("bus-schedules")]
        public async Task<IActionResult> GetBusSchedules()
        {
            var schedules = await _rideRepository.GetBusSchedulesAsync();
            return Ok(new { status = true, message = "Bus schedules retrieved successfully", schedules });
        }

        [HttpGet("available-rides")]
        public async Task<IActionResult> GetAvailableRides()
        {
            var rides = await _rideRepository.GetPendingRidesAsync();
            return Ok(new { status = true, message = "Available rides retrieved successfully", rides });
        }

        [HttpPut("{rideId}/status")]
        public async Task<IActionResult> UpdateRideStatus(int rideId, [FromBody] RideStatusUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var ride = await _rideRepository.UpdateRideStatusAsync(rideId, dto.Status);
            if (ride == null)
                return NotFound(new { status = false, message = "Ride not found." });

            return Ok(new { status = true, message = "Ride status updated successfully", ride });
        }

        [HttpPut("{rideId}/assign-driver/{driverId}")]
        public async Task<IActionResult> AssignDriverToRide(int rideId, int driverId)
        {
            var ride = await _rideRepository.AssignDriverToRideAsync(rideId, driverId);
            if (ride == null)
            {
                return BadRequest(new { status = false, message = "Ride not found or already assigned." });
            }

            return Ok(new { status = true, message = "Ride assigned to driver successfully", ride });
        }
    }
}
