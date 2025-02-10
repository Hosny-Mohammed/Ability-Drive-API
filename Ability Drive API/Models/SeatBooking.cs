using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ability_Drive_API.Data;

namespace Ability_Drive_API.Models
{
    public class SeatBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BusScheduleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsDisabledPassenger { get; set; }

        [Required]
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(20)]
        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

        // Navigation properties
        [ForeignKey("BusScheduleId")]
        public BusSchedule BusSchedule { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
