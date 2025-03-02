using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;

public interface IRideRepository
{
    Task<(bool IsSuccess, string Message, RideDTO? Ride)> CreateRideAsync(int userId, int driverId, RideRequestDTO dto, string voucherCode = null);
    Task<IEnumerable<RidesDTOForDriver>> GetPendingRidesByDriverIdAsync(int driverId); // For drivers to see available rides
    Task<Ride> UpdateRideStatusAsync(int rideId, string status, string cancelationReason, int? driverId = null);
    Task<IEnumerable<BusScheduleDTO>> GetBusSchedulesAsync();
    Task<SeatBookingDTO?> BookBusSeatAsync(int userId, int busScheduleId);
    Task<RideStatusUpdateDTO> GetRideStatusAsync(int rideId);
    //Task<Ride?> AssignDriverToRideAsync(int rideId, int driverId); // NEW: Assign ride to driver

}
