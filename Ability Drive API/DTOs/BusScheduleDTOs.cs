using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.DTOs
{
    public class BusScheduleDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bus number is required.")]
        [MaxLength(10, ErrorMessage = "Bus number cannot exceed 10 characters.")]
        public string BusNumber { get; set; }

        [Required(ErrorMessage = "Departure time is required.")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "From location is required.")]
        [MaxLength(100, ErrorMessage = "From location cannot exceed 100 characters.")]
        public string FromLocation { get; set; }

        [Required(ErrorMessage = "To location is required.")]
        [MaxLength(100, ErrorMessage = "To location cannot exceed 100 characters.")]
        public string ToLocation { get; set; }

        [Required(ErrorMessage = "Available normal seats are required.")]
        public int AvailableNormalSeats { get; set; }

        [Required(ErrorMessage = "Available disabled seats are required.")]
        public int AvailableDisabledSeats { get; set; }

        [Required(ErrorMessage = "Wheelchair accessibility status is required.")]
        public bool IsWheelchairAccessible { get; set; }

        public int Price { get; set; }
    }
}
