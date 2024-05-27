using Hangfire;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Pharmacy;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Services
{
	public class ReminderService : IReminderService
    {
        private readonly RahtkContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly INotificationSender _notificatioSender;

        public ReminderService(RahtkContext context, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, INotificationSender notificatioSender)
        {
            _context = context;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _notificatioSender = notificatioSender;
        }
        public async Task ScheduleReminder(ReminderEntity reminder)
        {
            // Calculate the next reminder date
            var nextReminderDate = reminder.ReminderDate.AddSeconds(reminder.ReminderIntervalDays);
            reminder.ReminderDate = nextReminderDate;
            _context.Reminders.Update(reminder);
            await _context.SaveChangesAsync();

            // Schedule the job with Hangfire
            _recurringJobManager.AddOrUpdate(
                reminder.Id.ToString(),
                () => SendReminder(reminder.Id),
                Cron.Minutely); // Schedule to run daily and check if the reminder date is today
        }
        public async Task SendReminder(int reminderId)
        {
            var reminder = await _context.Reminders.Include(r => r.Drug).Include(r=>r.User).FirstOrDefaultAsync(r => r.Id == reminderId);
            if (reminder == null) return;

            if (DateTime.UtcNow.Date == reminder.ReminderDate.Date)
            {
                await _notificatioSender.SendNotification(reminder.User.FcmToken,$"Take your drug named {reminder.Drug.Name}");
            }
        }
    }
}