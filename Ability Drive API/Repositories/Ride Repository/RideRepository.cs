using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using Ability_Drive_API.Repositories.Voucher_Repository;
using Ability_Drive_API.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ability_Drive_API.Repositories.Ride_Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IVoucherRepository _voucherRepository;

        public RideRepository(ApplicationDbContext context, IVoucherRepository voucherRepository)
        {
            _context = context;
            _voucherRepository = voucherRepository;
        }

        public async Task<(bool IsSuccess, string Message, Ride? Ride)> CreateRideAsync(int userId, int driverId, RideRequestDTO dto, string voucherCode = null)
        {
            // Retrieve the driver from the database including their preferred locations and last known location
            var driver = await _context.Drivers
                .Where(d => d.Id == driverId)
                .FirstOrDefaultAsync();

            if (driver == null)
            {
                return (false, "driver not found.", null);
            }

            // Ensure preferred locations are compared case-insensitively
            var preferredLocations = driver.PreferredLocations
                .Select(location => location.ToLower())
                .ToList();

            var destinationLower = dto.Destination.ToLower();

            // Check if the destination is in the driver's preferred locations
            if (!preferredLocations.Contains(destinationLower))
            {
                return (false, "the selected driver does not prefer this destination.", null);
            }

            // Check if the driver's last known location is far from the pickup location
            if (!string.IsNullOrEmpty(driver.LastKnownLocation) &&
                driver.LastKnownLocation.ToLower() != dto.PickupLocation.ToLower())
            {
                return (false, "the driver is too far from the pickup location.", null);
            }

            // Calculate ride cost using the static method in CalculateRideCostClass
            decimal rideCost = CalculateRideCostClass.CalculateRideCost(dto.PickupLocation, dto.Destination);

            // Apply voucher discount if available
            if (!string.IsNullOrEmpty(voucherCode))
            {
                var voucher = await _voucherRepository.GetVoucherByCodeAsync(voucherCode);
                if (voucher != null && voucher.ExpiryDate >= DateTime.UtcNow)
                {
                    var hasUsed = await _voucherRepository.HasUserUsedVoucherAsync(userId, voucherCode);
                    if (!hasUsed)
                    {
                        rideCost -= voucher.Discount;
                        rideCost = Math.Max(rideCost, 0); // Ensure cost doesn't go below zero
                    }
                    else
                    {
                        return (false, "voucher has already been used.", null);
                    }
                }
                else
                {
                    return (false, "invalid or expired voucher code.", null);
                }
            }

            // Create the ride (default status set to "Pending")
            Ride ride = new Ride
            {
                UserId = userId,
                DriverId = driverId,
                PickupLocation = dto.PickupLocation,
                Destination = dto.Destination,
                Status = "pending", // Default to Pending, can be updated later based on business logic
                RequestTime = DateTime.UtcNow,
                Cost = rideCost
            };

            // Save the ride to the database
            await _context.Rides.AddAsync(ride);
            await _context.SaveChangesAsync();

            return (true, "ride created successfully.", ride);
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
            };
        }



        public async Task<IEnumerable<BusSchedule>> GetBusSchedulesAsync()
        {
            return await _context.BusSchedules.ToListAsync();
        }


        public async Task<IEnumerable<RideDTOForOther>> GetPendingRidesByDriverIdAsync(int driverId)
        {
            return await _context.Rides
                                 .Where(r => r.Status == "Pending" && r.DriverId == driverId)
                                 .Select(r => new RideDTOForOther
                                 {
                                     PickupLocation = r.PickupLocation,
                                     Destination = r.Destination,
                                     Cost = r.Cost,
                                     Status = r.Status
                                 })
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
