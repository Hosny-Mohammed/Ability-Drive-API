using Ability_Drive_API.DTOs;
using Ability_Drive_API.Repositories.Driver_Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ability_Drive_API.Controllers
{
    [ApiController]
    [Route("api/driver")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;

        public DriverController(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DriverLoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var driver = await _driverRepository.AuthenticateDriverAsync(loginDto);
            if (driver == null)
                return Unauthorized("Invalid license number or password.");

            return Ok(new { message = "Login successful", driverId = driver.Id, driverName = driver.Name });
        }

        [HttpGet("profile/{driverId}")]
        public async Task<IActionResult> GetProfile(int driverId)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(driverId);
            if (driver == null)
                return NotFound("Driver not found.");

            return Ok(new
            {
                driverId = driver.Id,
                driverName = driver.Name,
                licenseNumber = driver.LicenseNumber,
                vehicleType = driver.VehicleType,
                rating = driver.Rating,
                isAvailable = driver.IsAvailable
            });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return Ok("Logged out successfully. (Client should clear stored driver ID)");
        }
    }
}
