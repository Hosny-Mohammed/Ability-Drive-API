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

        public async Task<User> RegisterAsync(UserRegisterDTO dto)
        {
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginAsync(UserLoginDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

            if (user == null ||  (dto.Password != user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return user;
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
