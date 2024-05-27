using Rahtk.Domain.Features.Pharmacy;

namespace Rahtk.Infrastructure.EF.Services
{
	public interface IReminderService
	{
        Task ScheduleReminder(ReminderEntity reminder);
    }
}