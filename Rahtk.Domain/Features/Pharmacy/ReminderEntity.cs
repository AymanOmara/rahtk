using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.User;

namespace Rahtk.Domain.Features.Pharmacy
{
    public class ReminderEntity
    {
        public int Id { get; set; }

        public int DrugId { get; set; }

        public DrugEntity? Drug { get; set; }

        public DateTime ReminderDate { get; set; } = DateTime.Now;

        public int ReminderIntervalDays { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }

        public RahtkUser? User { get; set; }
    }
}

