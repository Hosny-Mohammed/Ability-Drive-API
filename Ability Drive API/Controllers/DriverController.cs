﻿using Ability_Drive_API.DTOs;
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
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var driver = await _driverRepository.AuthenticateDriverAsync(loginDto);
            if (driver == null)
                return NotFound(new { status = false, message = "Invalid license number or password." });

            return Ok(new { status = true, message = "Login successful", driverId = driver.Id, driverName = driver.Name });
        }

        [HttpGet("profile/{driverId}")]
        public async Task<IActionResult> GetProfile(int driverId)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(driverId);
            if (driver == null)
                return NotFound(new { status = false, message = "Driver not found." });

            return Ok(new
            {
                status = true,
                message = "Driver profile retrieved successfully",
                driver = new
                {
                    driverId = driver.Id,
                    driverName = driver.Name,
                    licenseNumber = driver.LicenseNumber,
                    vehicleType = driver.VehicleType,
                    rating = driver.Rating,
                    isAvailable = driver.IsAvailable
                }
            });
        }
        [HttpPut("availability/{driverId}")]
        public async Task<IActionResult> UpdateAvailability(int driverId, [FromQuery] bool isAvailable)
        {
            var result = await _driverRepository.UpdateDriverAvailabilityAsync(driverId, isAvailable);
            if (!result) return NotFound(new { status = false, message = "Driver not found or update failed." });

            return Ok(new { status = true, message = "Driver availability updated successfully." });
        }

        [HttpGet("available-drivers")]
        public async Task<IActionResult> GetAllAvailableDrivers(
            [FromQuery] string? preferredLocation = null,
            [FromQuery] string? lastKnownLocation = null)
        {
            var drivers = await _driverRepository.GetAllAvailableDriversAsync(preferredLocation, lastKnownLocation);

            if (drivers == null || drivers.Count == 0)
            {
                return NotFound(new { status = false, message = "No available drivers found." });
            }

            return Ok(new { status = true, message = "Available drivers retrieved successfully", drivers });
        }
        [HttpPut("update-location/{driverId}")]
        public async Task<IActionResult> UpdateLastKnownLocation(int driverId, [FromQuery] string location)
        {
            var (success, message, lastKnownLocation) = await _driverRepository.UpdateLastKnownLocation(driverId, location);

            if (!success) return NotFound(new { status = false, message });

            return Ok(new { status = true, message, lastKnownLocation });
        }

    }
}
