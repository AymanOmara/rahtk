using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Application.Features.Reminder.mappers
{
    public static class AddReminderProdcutMapper
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