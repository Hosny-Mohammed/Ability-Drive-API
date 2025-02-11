using Ability_Drive_API.DTOs;
using Ability_Drive_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ability_Drive_API.Controllers
{
    [ApiController]
    [Route("api/creditcards")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        [HttpPost("add-payment-method/{userId}")]
        public async Task<IActionResult> AddPaymentMethod(int userId, [FromBody] CreditCardCreateDTO cardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            bool success = await _creditCardRepository.AddPaymentMethodAsync(cardDto, userId);

            if (!success)
                return StatusCode(500, new { status = false, message = "Failed to add payment method" });

            return Ok(new { status = true, message = "Payment method added successfully" });
        }
    }
}
