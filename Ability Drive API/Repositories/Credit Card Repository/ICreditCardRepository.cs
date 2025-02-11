using Ability_Drive_API.DTOs;
using System.Threading.Tasks;

namespace Ability_Drive_API.Repositories
{
    public interface ICreditCardRepository
    {
        Task<bool> AddPaymentMethodAsync(CreditCardCreateDTO cardDto, int userId);
    }
}
