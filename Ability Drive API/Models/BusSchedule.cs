using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.Models
{
    public class BusSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string BusNumber { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string FromLocation { get; set; }

        [Required]
        [MaxLength(100)]
        public string ToLocation { get; set; }

        [Required]
        public int TotalNormalSeats { get; set; }

        [Required]
        public int AvailableNormalSeats { get; set; }

        [Required]
        public int TotalDisabledSeats { get; set; }

        [Required]
        public int AvailableDisabledSeats { get; set; }

        [Required]
        public bool IsWheelchairAccessible { get; set; }

        [Required]
        public bool HasPrioritySeating { get; set; }

        [Required]
        public bool HasAudioAnnouncements { get; set; }

        // Navigation property
        public ICollection<SeatBooking> SeatBookings { get; set; } = new List<SeatBooking>();
    }
}
