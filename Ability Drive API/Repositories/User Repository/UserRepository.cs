using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ability_Drive_API.Repositories.User_Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserWithDetailsDTO> RegisterAsync(UserRegisterDTO dto)
        {
            // Create the user entity
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                IsDisabled = dto.IsDisabled,
                Password = dto.Password,
                CreatedAt = DateTime.UtcNow
            };

            // Add the user to the database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Return the user data as a DTO with empty rides and seat bookings
            var userDto = new UserWithDetailsDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsDisabled = user.IsDisabled,
                Rides = new List<RideDTOForOther>(), // Newly registered users have no rides
                SeatBookings = new List<SeatBookingDTOForOther>() // Newly registered users have no seat bookings
            };

            return userDto;
        }


        public async Task<UserWithDetailsDTO?> LoginAsync(UserLoginDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Rides)
                .Include(u => u.SeatBookings)
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

            if (user == null || (dto.Password != user.Password))
                return null;

            // Map the User to the DTO
            var userDto = new UserWithDetailsDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsDisabled = user.IsDisabled,
                Rides = user.Rides.Select(r => new RideDTOForOther
                {
                    Id = r.Id,
                    PickupLocation = r.PickupLocation,
                    Destination = r.Destination,
                    Cost = r.Cost,
                    Status = r.Status
                }),
                SeatBookings = user.SeatBookings.Select(sb => new SeatBookingDTOForOther
                {
                    Id = sb.Id,
                    BusScheduleId = sb.BusScheduleId,
                    IsDisabledPassenger = sb.IsDisabledPassenger,
                    BookingTime = sb.BookingTime,
                    Status = sb.Status
                })
            };

            return userDto;
        }


        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
