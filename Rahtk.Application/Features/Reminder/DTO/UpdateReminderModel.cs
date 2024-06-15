namespace Rahtk.Application.Features.Reminder.DTO
{
	public class UpdateReminderModel
	{
        public int id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<AddProductReminderModel>? Products { get; set; }

        public int ReminderIntervalDays { get; set; }
    }
}

