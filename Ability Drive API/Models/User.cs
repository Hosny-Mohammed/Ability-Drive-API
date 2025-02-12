using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Ride> Rides { get; set; } = new List<Ride>();
        public ICollection<SeatBooking> SeatBookings { get; set; } = new List<SeatBooking>();
    }
}
