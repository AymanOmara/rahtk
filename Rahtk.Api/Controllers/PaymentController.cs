using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.Payment.DTO;
using Rahtk.Application.Features.Payment.Service;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("add-payment")]
        public async Task<IActionResult> AddPayment([FromBody] CreatePaymentOptionModel paymentOptionModel)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _paymentService.CreatePayment(paymentOptionModel, email);
            return result.ToResult();
        }

        [HttpGet("get-payments")]
        public async Task<IActionResult> GetPayments()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _paymentService.GetPaymentOptions(email);
            return result.ToResult();
        }
    }
}

