using Ability_Drive_API.Data;
using Ability_Drive_API.Models;
using Ability_Drive_API.Repositories.Voucher_Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ability_Drive_API.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext _context;

        public VoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Voucher> GetVoucherByCodeAsync(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }

        public async Task<bool> HasUserUsedVoucherAsync(int userId, string code)
        {
            return await _context.UserVouchers
                .Include(uv => uv.Voucher)
                .AnyAsync(uv => uv.UserId == userId && uv.Voucher.Code == code);
        }
    }
}