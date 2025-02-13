using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.DTOs
{
    public class CreditCardCreateDTO
    {
        [Required(ErrorMessage = "Cardholder name is required.")]
        [MaxLength(50)]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
        [MaxLength(19)]
        public string CardNumber { get; set; } // Will be masked before saving

        [Required(ErrorMessage = "Expiry date is required.")]
        [MaxLength(7)]
        public string ExpiryDate { get; set; } // Format: MM/YYYY

        [Required(ErrorMessage = "Security Code (CVV) is required.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Security Code must be 3 or 4 digits.")]
        public string SecurityCode { get; set; } // CVV/CVC (not stored)

        [Required(ErrorMessage = "Zip Code is required.")]
        [MaxLength(10)]
        public string ZipCode { get; set; } // Billing ZIP code
    }
}
