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
        public async Task<List<DriverDTOGet>> GetAllAvailableDriversAsync(string? preferredLocation = null, string? lastKnownLocation = null)
        {
            var query = _context.Drivers.Where(d => d.IsAvailable);

            if (!string.IsNullOrEmpty(preferredLocation))
            {
                query = query.Where(d => d.PreferredLocations.Contains(preferredLocation));
            }

            if (!string.IsNullOrEmpty(lastKnownLocation))
            {
                query = query.Where(d => d.LastKnownLocation == lastKnownLocation);
            }

            var availableDrivers = await query
                .Select(d => new DriverDTOGet
                {
                    Id = d.Id,
                    Name = d.Name,
                    VehicleType = d.VehicleType,
                    IsAvailable = d.IsAvailable,
                    LastKnownLocation = d.LastKnownLocation,
                    PhoneNumber = d.PhoneNumber,
                    Rating = d.Rating,
                    PreferredLocations = d.PreferredLocations
                })
                .ToListAsync();

            return availableDrivers;
        }

        public async Task<bool> UpdateDriverAvailabilityAsync(int driverId, bool isAvailable)
        {
            var driver = await _context.Drivers.FindAsync(driverId);
            if (driver == null) return false;

            driver.IsAvailable = isAvailable;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Driver?> GetDriverByIdAsync(int driverId)
        {
            return await _context.Drivers
                .AsNoTracking() // Improves performance for read-only queries
                .FirstOrDefaultAsync(d => d.Id == driverId);
        }

    }
}
