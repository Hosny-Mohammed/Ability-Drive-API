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
            // Validate input
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    status = false,
                    message = "Invalid input data",
                    errors = ModelState
                });

            try
            {
                // Register user
                var userDto = await _userRepository.RegisterAsync(dto);

                // Return success response with user data
                return Ok(new
                {
                    status = true,
                    message = "User registered successfully",
                    user = userDto
                });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new
                {
                    status = false,
                    message = "Failed to register user",
                    error = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            // Validate input
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    status = false,
                    message = "Invalid input data",
                    errors = ModelState
                });

            try
            {
                // Attempt login
                var userDto = await _userRepository.LoginAsync(dto);

                // Check if login was successful
                if (userDto == null)
                    return Unauthorized(new
                    {
                        status = false,
                        message = "Invalid phone number or password."
                    });

                // Return success response with user data
                return Ok(new
                {
                    status = true,
                    message = "Login successful",
                    user = userDto
                });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new
                {
                    status = false,
                    message = "An error occurred while processing your request.",
                    error = ex.Message
                });
            }
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
