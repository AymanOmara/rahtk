using Rahtk.Application.Features.product.DTO;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Reminder.Service
{
	public interface IReminderService
	{
		Task<BaseResponse<ICollection<ReadReminderModel>>> GetAllReminders(string email);

        Task<BaseResponse<ReadReminderModel>> AddReminder(AddReminderModel reminderModel,string email);

        Task<BaseResponse<ReadReminderModel>> UpdateReminder(UpdateReminderModel reminderModel, string email);

        Task<BaseResponse<bool>> DeleteReminder(UpdateReminderModel reminderModel, string email);
    }
}