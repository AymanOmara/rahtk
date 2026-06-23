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
    public class AddressController(IAddressService addressService) : ControllerBase
    {
        
        [HttpPost("create-address")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressModel addressModel) {

            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await addressService.CreateAddress(addressModel, email ?? "");
            return result.ToResult();
        }
        [HttpGet("get-address")]
        public async Task<IActionResult> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await addressService.GetAddresses(email ?? "");
            return result.ToResult();
        }
    }
}