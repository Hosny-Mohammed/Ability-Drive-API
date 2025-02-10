using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.DTOs
{
    public class RideRequestDTO
    {
        [Required(ErrorMessage = "Pickup location is required.")]
        [MaxLength(100, ErrorMessage = "Pickup location cannot exceed 100 characters.")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        [MaxLength(100, ErrorMessage = "Destination cannot exceed 100 characters.")]
        public string Destination { get; set; }

        public int? PaymentMethodId { get; set; }
    }

    public class RideStatusUpdateDTO
    {
        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }  // Confirm/Cancel/Complete

        [MaxLength(200, ErrorMessage = "Reason cannot exceed 200 characters.")]
        public string? Reason { get; set; }
    }
}
