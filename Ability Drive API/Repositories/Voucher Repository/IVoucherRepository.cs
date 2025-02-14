using Ability_Drive_API.Models;

namespace Ability_Drive_API.Repositories.Voucher_Repository
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetVoucherByCodeAsync(string code);
        Task<bool> HasUserUsedVoucherAsync(int userId, string code);
    }
}
