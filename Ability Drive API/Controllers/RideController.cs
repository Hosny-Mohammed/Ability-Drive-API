using Ability_Drive_API.DTOs;
using Ability_Drive_API.Repositories.Ride_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> CreateRide([FromBody] RideRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var ride = await _rideRepository.CreateRideAsync(userId, dto);
            return Ok(ride);
        }

        [HttpPut("{rideId}/status")]
        public async Task<IActionResult> UpdateRideStatus(int rideId, [FromBody] RideStatusUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ride = await _rideRepository.UpdateRideStatusAsync(rideId, dto.Status);
            return Ok(ride);
        }
    }
}
