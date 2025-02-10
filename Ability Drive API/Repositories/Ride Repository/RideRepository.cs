using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ability_Drive_API.Repositories.Ride_Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly ApplicationDbContext _context;

        public RideRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ride> CreateRideAsync(int userId, RideRequestDTO dto)
        {
            var ride = new Ride
            {
                UserId = userId,
                PickupLocation = dto.PickupLocation,
                Destination = dto.Destination,
                Status = "Pending",
                RequestTime = DateTime.UtcNow
            };

            await _context.Rides.AddAsync(ride);
            await _context.SaveChangesAsync();
            return ride;
        }

        public async Task<Ride?> GetRideByIdAsync(int rideId)
        {
            return await _context.Rides.FindAsync(rideId);
        }

        public async Task<IEnumerable<Ride>> GetPendingRidesAsync()
        {
            return await _context.Rides
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }

        public async Task<Ride> UpdateRideStatusAsync(int rideId, string status, int? driverId = null)
        {
            var ride = await _context.Rides.FindAsync(rideId);
            if (ride == null) throw new KeyNotFoundException("Ride not found");

            ride.Status = status;
            ride.DriverId = driverId;

            await _context.SaveChangesAsync();
            return ride;
        }
    }
}
