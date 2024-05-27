using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.Address.DTO;
using Rahtk.Application.Features.Address.Service;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
		{
            _addressService = addressService;
		}
        [HttpPost("create-address")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressModel addressModel) {

            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _addressService.CreateAddress(addressModel, email ?? "");
            return result.ToResult();
        }
        [HttpGet("get-address")]
        public async Task<IActionResult> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _addressService.GetAddresses(email ?? "");
            return result.ToResult();
        }
    }
}