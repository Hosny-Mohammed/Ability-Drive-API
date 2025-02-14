using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;

public interface IRideRepository
{
    Task<Ride> CreateRideAsync(int userId, int driverId, RideRequestDTO dto, string voucherCode = null);
    Task<Ride?> GetRideByIdAsync(int rideId);
    Task<IEnumerable<Ride>> GetPendingRidesAsync(); // For drivers to see available rides
    Task<Ride> UpdateRideStatusAsync(int rideId, string status, int? driverId = null);
    Task<IEnumerable<BusSchedule>> GetBusSchedulesAsync();
    Task<SeatBookingDTO?> BookBusSeatAsync(int userId, int busScheduleId);
    Task<Ride?> AssignDriverToRideAsync(int rideId, int driverId); // NEW: Assign ride to driver
    
}
