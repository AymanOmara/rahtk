using Rahtk.Application.Features.Reminder.DTO;

namespace Rahtk.Application.Features.product.DTO
{
	public class AddReminderModel
	{
        public string Title { get; set; } = string.Empty;

        public ICollection<AddProductReminderModel>? Products { get; set; }

        public int ReminderIntervalDays { get; set; }
    }
}