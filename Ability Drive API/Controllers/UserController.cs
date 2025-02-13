using Ability_Drive_API.DTOs;
using Ability_Drive_API.Repositories.User_Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ability_Drive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var user = await _userRepository.RegisterAsync(dto);

            if (user == null)
                return StatusCode(500, new { status = false, message = "Failed to register user" });

            return Ok(new { status = true, message = "User registered successfully", user });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            var user = await _userRepository.LoginAsync(dto);
            if (user == null)
                return Unauthorized(new { status = false, message = "Invalid email or password." });

            return Ok(new { status = true, message = "Login successful", userId = user.Id, userName = $"{user.FirstName} {user.LastName}" });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { status = false, message = "User not found." });

            return Ok(new
            {
                status = true,
                message = "User retrieved successfully",
                user = user
            });
        }
    }
}
