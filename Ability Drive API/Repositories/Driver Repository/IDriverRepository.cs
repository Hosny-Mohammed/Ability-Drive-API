using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using System.Threading.Tasks;

namespace Ability_Drive_API.Repositories.Driver_Repository
{
    public interface IDriverRepository
    {
        Task<Driver?> AuthenticateDriverAsync(DriverLoginDTO loginDto);
        Task<Driver?> GetDriverByIdAsync(int driverId);
        Task<List<DriverDTOGet>> GetAllAvailableDriversAsync(string? preferredLocation = null, string? lastKnownLocation = null);
        Task<bool> UpdateDriverAvailabilityAsync(int driverId, bool isAvailable);
        Task<(bool success, string message, string lastKnownLocation)> UpdateLastKnownLocation(int driverId, string location);
    }
}
