using Rahtk.Domain.Features.Products;

namespace Rahtk.Domain.Features.Reminder
{
	public class ProductReminder
	{
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }

        public int ReminderId { get; set; }
        public ReminderEntity? Reminder { get; set; }
    }
}

