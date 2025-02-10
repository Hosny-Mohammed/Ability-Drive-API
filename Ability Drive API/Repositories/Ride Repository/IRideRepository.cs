using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;

namespace Ability_Drive_API.Repositories.Ride_Repository
{
    public interface IRideRepository
    {
        Task<Ride> CreateRideAsync(int userId, RideRequestDTO dto);
        Task<Ride?> GetRideByIdAsync(int rideId);
        Task<IEnumerable<Ride>> GetPendingRidesAsync();
        Task<Ride> UpdateRideStatusAsync(int rideId, string status, int? driverId = null);
    }
}
