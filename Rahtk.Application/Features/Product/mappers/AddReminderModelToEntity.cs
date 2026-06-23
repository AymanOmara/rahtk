using Rahtk.Application.Features.Reminder.Mapper;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Application.Features.Product.mappers
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