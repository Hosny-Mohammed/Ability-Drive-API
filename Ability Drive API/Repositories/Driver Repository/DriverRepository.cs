using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ability_Drive_API.Repositories.Driver_Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApplicationDbContext _context;

        public DriverRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Driver?> AuthenticateDriverAsync(DriverLoginDTO loginDto)
        {
            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.LicenseNumber == loginDto.LicenseNumber);

            if (driver == null || driver.Password != loginDto.Password)
                return null; // Authentication failed

            return driver; // Successful login
        }
        public async Task<List<DriverDTOGet>> GetAllAvailableDriversAsync()
        {
            var availableDrivers = await _context.Drivers
                .Where(d => d.IsAvailable)
                .Select(d => new DriverDTOGet
                {
                    Id = d.Id,
                    Name = d.Name,
                    VehicleType = d.VehicleType,
                    IsAvailable = d.IsAvailable,
                    LastKnownLocation = d.LastKnownLocation,
                    PhoneNumber = d.PhoneNumber,
                    Rating = d.Rating,
                    PreferredLocations = d.PreferredLocations // Include preferred locations
                })
                .ToListAsync();

            return availableDrivers;
        }


        public async Task<Driver?> GetDriverByIdAsync(int driverId)
        {
            return await _context.Drivers
                .AsNoTracking() // Improves performance for read-only queries
                .FirstOrDefaultAsync(d => d.Id == driverId);
        }

    }
}
