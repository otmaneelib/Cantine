using Cantine.Core.Dtos;
using Cantine.Core.Interfaces.Services;
using Cantine.Core.Interfaces.Validation;
using Microsoft.AspNetCore.Mvc;

namespace CantineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;
        private readonly IValidatorService _validatorService;

        public BillingController(IBillingService billingService, IValidatorService validatorService)
        {
            _billingService = billingService;
            _validatorService = validatorService;
        }

        [HttpPost("credit")]
        public async Task<IActionResult> CreditClientAccount([FromBody] CreditRequestDTO request)
        {
            // Validation du DTO
            var validationResult = _validatorService.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _billingService.CreditClientAccount(request.ClientId, request.Amount);
            return Ok();
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PayMeal([FromBody] PayMealRequestDTO request)
        {
            // Validation du DTO
            var validationResult = _validatorService.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var ticket = await _billingService.PayMeal(request.ClientId, request.Meal);
            return Ok(ticket);
        }
    }
}
