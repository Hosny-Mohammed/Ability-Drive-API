using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
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
            Ride ride;

            if (dto.BusScheduleId.HasValue)
            {
                var seatBooking = await BookBusSeatAsync(userId, dto.BusScheduleId.Value);
                if (seatBooking == null)
                {
                    throw new Exception("No available seats for the selected bus.");
                }

                ride = new Ride
                {
                    UserId = userId,
                    PickupLocation = dto.PickupLocation,
                    Destination = dto.Destination,
                    Status = "Confirmed",
                    RequestTime = DateTime.UtcNow
                };
            }
            else
            {
                ride = new Ride
                {
                    UserId = userId,
                    PickupLocation = dto.PickupLocation,
                    Destination = dto.Destination,
                    Status = "Pending",
                    RequestTime = DateTime.UtcNow
                };

                await _context.Rides.AddAsync(ride);
                await _context.SaveChangesAsync();
            }

            return ride;
        }

        public async Task<SeatBookingDTO?> BookBusSeatAsync(int userId, int busScheduleId)
        {
            var busSchedule = await _context.BusSchedules.FindAsync(busScheduleId);
            if (busSchedule == null || busSchedule.AvailableNormalSeats == 0)
            {
                return null; // No available seats
            }

            busSchedule.AvailableNormalSeats -= 1;

            var seatBooking = new SeatBooking
            {
                BusScheduleId = busScheduleId,
                UserId = userId,
                IsDisabledPassenger = false,
                BookingTime = DateTime.UtcNow,
                Status = BookingStatus.Confirmed
            };

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
