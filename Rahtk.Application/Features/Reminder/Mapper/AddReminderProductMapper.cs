using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Application.Features.Reminder.Mapper
{
    public static class AddReminderProductMapper
    {
        public static ProductReminder ToEntity(this AddProductReminderModel model)
        {
            return new ProductReminder
            {
                ProductId = model.id,
            };
        }
    }
}
