using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.Order;
using Rahtk.Domain.Features.Order;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
		{
            _orderService = orderService;
		}

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel order)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderService.CreateOrder(order, email ?? "");
            return result.ToResult();
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderService.GetOrders(email ?? "");
            return result.ToResult();
        }
    }
}

