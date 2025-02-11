using Ability_Drive_API.Data;
using Ability_Drive_API.DTOs;
using Ability_Drive_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ability_Drive_API.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly ApplicationDbContext _context;

        public CreditCardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPaymentMethodAsync(CreditCardCreateDTO cardDto, int userId)
        {
            try
            {
                var maskedCardNumber = "**** **** **** " + cardDto.CardNumber[^4..];

                var creditCard = new CreditCard
                {
                    UserId = userId,
                    CardHolderName = cardDto.CardHolderName,
                    CardNumber = maskedCardNumber,
                    ExpiryDate = cardDto.ExpiryDate,
                    ZipCode = cardDto.ZipCode
                };

                await _context.CreditCards.AddAsync(creditCard);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
