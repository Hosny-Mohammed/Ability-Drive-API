using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability_Drive_API.Models
{
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        // Navigation properties
        public ICollection<UserVoucher> UserVouchers { get; set; }
    }
}