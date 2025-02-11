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

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateRide(int userId, [FromBody] RideRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ride = await _rideRepository.CreateRideAsync(userId, dto);
            return Ok(ride);
        }

        [HttpGet("bus-schedules")]
        public async Task<IActionResult> GetBusSchedules()
        {
            var schedules = await _rideRepository.GetBusSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("available-rides")]
        public async Task<IActionResult> GetAvailableRides()
        {
            var rides = await _rideRepository.GetPendingRidesAsync();
            return Ok(rides);
        }

        [HttpPut("{rideId}/status")]
        public async Task<IActionResult> UpdateRideStatus(int rideId, [FromBody] RideStatusUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ride = await _rideRepository.UpdateRideStatusAsync(rideId, dto.Status);
            if (ride == null)
                return NotFound("Ride not found.");

            return Ok(ride);
        }

        [HttpPut("{rideId}/assign-driver/{driverId}")]
        public async Task<IActionResult> AssignDriverToRide(int rideId, int driverId)
        {
            var ride = await _rideRepository.AssignDriverToRideAsync(rideId, driverId);
            if (ride == null)
            {
                return BadRequest("Ride not found or already assigned.");
            }

            return Ok(new { message = "Ride assigned to driver successfully", ride });
        }
    }
}
