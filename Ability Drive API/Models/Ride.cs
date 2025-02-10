using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.Models
{
    public class Ride
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int? DriverId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PickupLocation { get; set; }

        [Required]
        [MaxLength(100)]
        public string Destination { get; set; }

        [Required]
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";  // Pending/Confirmed/InProgress/Completed/Cancelled

        [MaxLength(200)]
        public string? CancellationReason { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("DriverId")]
        public Driver? Driver { get; set; }
    }
}
