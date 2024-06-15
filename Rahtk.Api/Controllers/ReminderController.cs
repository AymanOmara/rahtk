using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.product.DTO;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Application.Features.Reminder.Service;

namespace Rahtk.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
		{
            _reminderService = reminderService;
		}

        [HttpGet("get-reminders")]
        public async Task<IActionResult> GetReminders()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reminderService.GetAllReminders(email ?? "");
            return result.ToResult();
        }

        [HttpPost("add-reminder")]
        public async Task<IActionResult> AddReminder([FromBody] AddReminderModel model)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reminderService.AddReminder(model, email ?? "");
            return result.ToResult();
        }

        [HttpPut("update-reminder")]
        public async Task<IActionResult> UpdateReminder([FromBody] UpdateReminderModel model)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reminderService.UpdateReminder(model, email ?? "");
            return result.ToResult();
        }

        [HttpDelete("delete-reminder")]
        public async Task<IActionResult> DeleteReminder([FromBody] UpdateReminderModel model)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reminderService.DeleteReminder(model, email ?? "");
            return result.ToResult();
        }
    }
}