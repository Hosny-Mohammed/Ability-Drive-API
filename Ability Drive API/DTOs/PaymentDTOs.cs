using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.DTOs
{
    public class PaymentMethodCreateDTO
    {
        [Required(ErrorMessage = "Payment type is required.")]
        [MaxLength(20, ErrorMessage = "Payment type cannot exceed 20 characters.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Payment details are required.")]
        [MaxLength(100, ErrorMessage = "Payment details cannot exceed 100 characters.")]
        public string Details { get; set; }
    }

    public class PaymentMethodResponseDTO
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string MaskedDetails { get; set; }  // e.g., ****-****-****-1234
    }
}
