using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;

namespace Ability_Drive_API.Repositories.User_Repository
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(UserRegisterDTO dto);
        Task<User> LoginAsync(UserLoginDTO dto);
        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> UserExistsAsync(string phoneNumber);
    }
}
