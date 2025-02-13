using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability_Drive_API.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleType { get; set; }

        [Required]
        [MaxLength(20)]
        public string VehicleRegistration { get; set; }

        [Required]
        [Column(TypeName = "decimal(3, 2)")]
        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal Rating { get; set; } = 5.0m;

        [Required]
        public bool IsAvailable { get; set; } = true;

        [MaxLength(100)]
        public string? LastKnownLocation { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public List<string> PreferredLocations { get; set; } = new List<string>(); // Preferred locations in Egypt

        // Navigation property
        public ICollection<Ride> Rides { get; set; } = new List<Ride>();
    }
}
