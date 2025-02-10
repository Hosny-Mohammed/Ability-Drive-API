using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public decimal Rating { get; set; } = 5.0m;

        [Required]
        public bool IsAvailable { get; set; } = true;

        [MaxLength(100)]
        public string? CurrentLocation { get; set; }

        // Navigation property
        public ICollection<Ride> Rides { get; set; } = new List<Ride>();
    }
}
