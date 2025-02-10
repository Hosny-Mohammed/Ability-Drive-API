using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ability_Drive_API.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }  // CreditCard, Paytm, Cash

        [Required]
        [MaxLength(100)]
        public string Details { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
