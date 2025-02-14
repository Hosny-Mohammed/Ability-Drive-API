using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability_Drive_API.Models
{
    public class UserVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int VoucherId { get; set; }

        [Required]
        public DateTime UsedDate { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("VoucherId")]
        public Voucher Voucher { get; set; }
    }
}