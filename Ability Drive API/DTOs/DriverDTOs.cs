using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.DTOs
{
    public class DriverLoginDTO
    {
        [Required(ErrorMessage = "License number is required.")]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }
    }
}
