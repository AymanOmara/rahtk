using Rahtk.Application.Features.product.mappers;

namespace Rahtk.Application.Features.Reminder.DTO
{
	public class ReadReminderModel
	{
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<ReadProductModel>? Products { get; set; }

        public DateTime ReminderDate { get; set; } = DateTime.Now;

        public int ReminderIntervalDays { get; set; }
    }
}