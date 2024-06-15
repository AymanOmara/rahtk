using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Contracts.Features.Reminder
{
    public interface IReminderRepository
    {
        void ScheduleReminder(ReminderEntity reminder);

        Task<ReminderEntity> AddReminder(ReminderEntity reminder, string userEmail);

        Task<ICollection<ReminderEntity>> GetAllReminders(string userEmail);

        Task<ReminderEntity> UpdateReminder(ReminderEntity reminder, string userEmail);

        Task DeleteReminder(ReminderEntity reminder, string userEmail);
    }
}