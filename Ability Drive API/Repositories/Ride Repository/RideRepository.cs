using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using Ability_Drive_API.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            // Calculate ride cost using the static method in CalculateRideCostClass
            decimal rideCost = Ability_Drive_API.Service.CalculateRideCostClass.CalculateRideCost(dto.PickupLocation, dto.Destination);

            // Create the ride (default status set to "Pending")
            Ride ride = new Ride
            {
                UserId = userId,
                PickupLocation = dto.PickupLocation,
                Destination = dto.Destination,
                Status = "Pending", // Default to Pending, can be updated later based on business logic
                RequestTime = DateTime.UtcNow,
                Cost = rideCost
            };

            // Save the ride to the database
            await _context.Rides.AddAsync(ride);
            await _context.SaveChangesAsync();

            return ride;
        }



        private decimal CalculateRideCostClass(string pickupLocation, string destination)
        {
            throw new NotImplementedException();
        }

        public async Task<SeatBookingDTO?> BookBusSeatAsync(int userId, int busScheduleId)
        {
            // Fetch user details to determine if they are disabled
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Fetch the bus schedule
            var busSchedule = await _context.BusSchedules.FindAsync(busScheduleId);
            if (busSchedule == null)
            {
                throw new Exception("Bus schedule not found.");
            }

            // Check availability based on the user's disabled status
            if (user.IsDisabled)
            {
                if (busSchedule.AvailableDisabledSeats == 0)
                {
                    return null; // No available seats for disabled passengers
                }

                busSchedule.AvailableDisabledSeats -= 1;
            }
            else
            {
                if (busSchedule.AvailableNormalSeats == 0)
                {
                    return null; // No available seats for normal passengers
                }

                busSchedule.AvailableNormalSeats -= 1;
            }

            // Create a new seat booking
            var seatBooking = new SeatBooking
            {
                BusScheduleId = busScheduleId,
                UserId = userId,
                IsDisabledPassenger = user.IsDisabled, // Set based on user's profile
                BookingTime = DateTime.UtcNow,
                Status = BookingStatus.Confirmed
            };

            // Save changes to the database
            _context.BusSchedules.Update(busSchedule);
            await _context.SeatBookings.AddAsync(seatBooking);
            await _context.SaveChangesAsync();

            // Return a DTO instead of the full entity
            return new SeatBookingDTO
            {
                Id = seatBooking.Id,
                BusScheduleId = seatBooking.BusScheduleId,
                UserId = seatBooking.UserId,
                IsDisabledPassenger = seatBooking.IsDisabledPassenger,
                BookingTime = seatBooking.BookingTime,
                Status = seatBooking.Status
            };
        }



        public async Task<IEnumerable<BusSchedule>> GetBusSchedulesAsync()
        {
            return await _context.BusSchedules.ToListAsync();
        }

        public async Task<Ride?> GetRideByIdAsync(int rideId)
        {
            return await _context.Rides.FindAsync(rideId);
        }

        public async Task<IEnumerable<Ride>> GetPendingRidesAsync()
        {
            return await _context.Rides
                .Where(r => r.Status == "Pending" && r.DriverId == null)
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

        public async Task<Ride?> AssignDriverToRideAsync(int rideId, int driverId)
        {
            var ride = await _context.Rides.FindAsync(rideId);
            if (ride == null || ride.Status != "Pending" || ride.DriverId != null)
            {
                return null; // Ride not found, already assigned, or not available
            }

            ride.DriverId = driverId;
            ride.Status = "Confirmed";

            await _context.SaveChangesAsync();
            return ride;
        }
    }
}
