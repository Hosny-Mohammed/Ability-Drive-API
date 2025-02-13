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

        [HttpPost("AddCard/{userId}")]
        public async Task<IActionResult> AddCard(int userId, [FromBody] CreditCardCreateDTO cardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { status = false, message = "Invalid input data", errors = ModelState });

            bool success = await _creditCardRepository.AddCardAsync(cardDto, userId);

            if (!success)
                return StatusCode(500, new { status = false, message = "Failed to add payment method" });

            return Ok(new { status = true, message = "Card added successfully" });
        }
    }
}
