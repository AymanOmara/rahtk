using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.Products;
using Rahtk.Domain.Features.User;

namespace Rahtk.Domain.Features.Reminder
{
    public class ReminderEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<ProductReminder>? Products { get; set; }

        public DateTime ReminderDate { get; set; } = DateTime.Now;

        public int ReminderIntervalDays { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }

        public RahtkUser? User { get; set; }
    }
}