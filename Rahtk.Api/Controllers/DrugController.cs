using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.Drug;
using Rahtk.Application.Features.Drug.DTO;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drugService;
        public DrugController(IDrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpPost("create-drug")]
        public async Task<IActionResult> CreateDrug([FromForm] AddDrugModel model)
        {
            var result = await _drugService.AddDrug(model);
            return result.ToResult();
        }

        [HttpGet("get-drugs")]
        public async Task<IActionResult> GetDrugs()
        {
            var result = await _drugService.GetDrugs();
            return result.ToResult();
        }

        [HttpPost("add-reminder")]
        public async Task<IActionResult> AddReminder([FromBody] AddReminderModel model)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _drugService.AddReminder(model, email ?? "");
            return result.ToResult();
        }
    }
}