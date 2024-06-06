using Hangfire;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Pharmacy;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Services
{
	public class ReminderService : IReminderService
    {
        private readonly RahtkContext _context;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly INotificationSender _notificatioSender;

        public ReminderService(RahtkContext context, IRecurringJobManager recurringJobManager, INotificationSender notificatioSender)
        {
            _context = context;
            _recurringJobManager = recurringJobManager;
            _notificatioSender = notificatioSender;
        }
        public async Task ScheduleReminder(ReminderEntity reminder)
        {
           
            var nextReminderDate = reminder.ReminderDate.AddHours(reminder.ReminderIntervalDays);
            reminder.ReminderDate = nextReminderDate;
            _context.Reminders.Update(reminder);
            await _context.SaveChangesAsync();

            
            _recurringJobManager.AddOrUpdate(
                reminder.Id.ToString(),
                () => SendReminder(reminder.Id),
                Cron.Hourly
                );
        }
        public async Task SendReminder(int reminderId)
        {
            var reminder = await _context.Reminders.Include(r => r.Drug).Include(r=>r.User).FirstOrDefaultAsync(r => r.Id == reminderId);
            if (reminder == null) return;

            if (DateTime.UtcNow.Date == reminder.ReminderDate.Date)
            {
                await _notificatioSender.SendNotification(reminder.User.FcmToken, $"Take your drug named {reminder.Drug.Name}");
                await _context.Notifications.AddAsync(new NotificationEntity { UserId = reminder.UserId, Title = "Rahtk Medicine Center", Body = $"Take your drug named {reminder.Drug.Name}" });
                await _context.SaveChangesAsync();
            }
            else if (DateTime.UtcNow.Date.AddHours(1) == reminder.ReminderDate.Date)
            {
                await _notificatioSender.SendNotification(reminder.User.FcmToken, $"Reminder: Your drug named {reminder.Drug.Name} needs to be taken tomorrow.");
                await _context.Notifications.AddAsync(new NotificationEntity { UserId = reminder.UserId, Title = "Rahtk Medicine Center", Body = $"Reminder: Your drug named {reminder.Drug.Name} needs to be taken tomorrow." });
                await _context.SaveChangesAsync();
            }
        }
    }
}