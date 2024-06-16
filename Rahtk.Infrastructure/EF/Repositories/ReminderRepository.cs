using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.Reminder;
using Rahtk.Domain.Features.Reminder;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly RahtkContext _context;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly INotificationSender _notificatioSender;
        public readonly UserManager<RahtkUser> _userManager;
        public ReminderRepository(
            RahtkContext context,
            IRecurringJobManager recurringJobManager,
            INotificationSender notificatioSender,
            UserManager<RahtkUser> userManager
            )
        {
            _context = context;
            _recurringJobManager = recurringJobManager;
            _notificatioSender = notificatioSender;
            _userManager = userManager;
        }

        public async Task DeleteReminder(ReminderEntity reminder, string userEmail)
        {
            _recurringJobManager.RemoveIfExists(reminder.Id.ToString());
            _context.Entry(reminder).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<ReminderEntity>> GetAllReminders(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var reminders = await _context.Reminders.Include(pr => pr.Products).ThenInclude(pr=> pr.Product).Where(re => re.UserId == user.Id).ToListAsync();
            return reminders;
        }

        public void ScheduleReminder(ReminderEntity reminder)
        {
            _recurringJobManager.AddOrUpdate(
                reminder.Id.ToString(),
                    () => SendReminder(reminder.Id),
                    Cron.Minutely
            );
        }

        public async Task SendReminder(int reminderId)
        {
            var reminder = await _context.Reminders.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == reminderId);
            if (reminder == null) return;

            if (DateTime.UtcNow.Date == reminder.ReminderDate.Date)
            {
                await _notificatioSender.SendNotification(reminder.User.FcmToken, $"Take your Order named {reminder.Title}");
                await _context.Notifications.AddAsync(new NotificationEntity { UserId = reminder?.UserId, Title = "Rahtk Notification Center", Body = $"re-order your list {reminder.Title}" });
                await _context.SaveChangesAsync();
            }
            else if (DateTime.UtcNow.Date.AddMinutes(1) == reminder.ReminderDate.Date)
            {
                await _notificatioSender.SendNotification(reminder.User.FcmToken, $"Reminder: don't forget to re-order your list {reminder.Title} tomorrow.");
                await _context.Notifications.AddAsync(new NotificationEntity { UserId = reminder?.UserId, Title = "Rahtk Notification Center", Body = $"Reminder: don't forget to re-order your list {reminder.Title} tomorrow." });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReminderEntity> UpdateReminder(ReminderEntity reminder, string userEmail)
        {
            ICollection<int> products = new List<int>(collection: reminder?.Products.Select(e => e.ProductId).ToList());

            var user = await _userManager.FindByEmailAsync(userEmail);
            var nextReminderDate = reminder.ReminderDate.AddMinutes(reminder.ReminderIntervalDays);
            var productReminders = await _context.ProductReminder.Include(p=>p.Product).Where(e => e.ReminderId == reminder.Id).ToListAsync();
            foreach (var item in productReminders) {

                _context.Entry(item).State = EntityState.Deleted;
            }

            reminder.ReminderDate = nextReminderDate;
            reminder.UserId = user.Id;
            reminder.Products.Clear();
            foreach (var product in products)
            {
                var dbProduct = await _context.Products.FirstOrDefaultAsync(pr => pr.Id == product);
                var productReminder = new ProductReminder { ProductId = dbProduct.Id, Product = dbProduct, ReminderId = reminder.Id };
                reminder.Products.Add(productReminder);
                _context.Entry(productReminder).State = EntityState.Added;
            }
            _context.Entry(reminder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            ScheduleReminder(reminder);
            return reminder;
        }

        public async Task<ReminderEntity> AddReminder(ReminderEntity reminder, string userEmail)
        {
            ICollection<int> products = new List<int>(collection: reminder?.Products?.Select(e => e.ProductId).ToList());
            var user = await _userManager.FindByEmailAsync(userEmail);
            var nextReminderDate = reminder.ReminderDate.AddHours(reminder.ReminderIntervalDays);
            reminder.ReminderDate = nextReminderDate;
            reminder.UserId = user.Id;
            reminder.Products = new List<ProductReminder> { };
            foreach (var product in products)
            {
                var dbProduct = await _context.Products.FirstOrDefaultAsync(pr => pr.Id == product);
                reminder.Products.Add(new ProductReminder{ ProductId = dbProduct.Id,Product = dbProduct });
            }

            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
            ScheduleReminder(reminder);
            return reminder;
        }
    }
}