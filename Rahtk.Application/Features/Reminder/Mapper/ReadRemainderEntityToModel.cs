using Rahtk.Application.Features.product.mappers;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Application.Features.Reminder.Mapper
{
    public static class ReadRemainderEntityToModel
    {
        public static ReadReminderModel ToModel(this ReminderEntity entity)
        {
            return new ReadReminderModel
            {
                Id = entity.Id,
                ReminderDate = entity.ReminderDate,
                Title = entity.Title,
                ReminderIntervalDays = entity.ReminderIntervalDays,
                Products = entity.Products?.Select(pr=>pr.Product.ToModel()).ToList()
            };
        }
    }
}