using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability_Drive_API.Models
{
    public class CreditCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CardHolderName { get; set; }

        [Required]
        [MaxLength(19)]
        public string CardNumber { get; set; } // Masked format: **** **** **** 1234

        [Required]
        [MaxLength(7)] // Format: MM/YYYY
        public string ExpiryDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; } // New field for Zip Code

        // Navigation property
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
