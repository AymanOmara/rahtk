using Rahtk.Application.Features.product.mappers;
using Rahtk.Application.Features.Reminder.mappers;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Application.Features.product.DTO
{
    public static class AddReminderModelToEntity
    {
        public static ReminderEntity ToEntity(this AddReminderModel model)
        {
            return new ReminderEntity
            {
                Title = model.Title,
                Products = model?.Products?.Select(p=>p.ToEntity()).ToList(),
                ReminderIntervalDays = model.ReminderIntervalDays,
            };
        }
    }
}