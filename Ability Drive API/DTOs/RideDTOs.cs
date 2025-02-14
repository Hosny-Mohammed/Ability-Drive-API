using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.DTOs
{
    public class RideRequestDTO
    {
        [Required(ErrorMessage = "Pickup location is required.")]
        [MaxLength(100)]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        [MaxLength(100)]
        public string Destination { get; set; }

    }


    public class RideStatusUpdateDTO
    {
        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }  // Confirm/Cancel/Complete

        [MaxLength(200, ErrorMessage = "Reason cannot exceed 200 characters.")]
        public string? Reason { get; set; }
    }
    public class RideDTOForOther
    {
        public int Id { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
    }
    public class RideDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DriverId { get; set; }
        public string PickupLocation { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public DateTime RequestTime { get; set; }
    }

}
