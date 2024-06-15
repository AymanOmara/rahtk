using Rahtk.Application.Features.product.mappers;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Application.Features.Reminder.mappers;
using Rahtk.Domain.Features.Reminder;

namespace Rahtk.Application.Features.Reminder.Mapper
{
	public static class UpdateReminderToEntity
	{
		public static ReminderEntity ToEntity(this UpdateReminderModel model) {
			return new ReminderEntity
			{
				Id = model.id,
				Title = model.Title,
                Products = model?.Products?.Select(pr => pr.ToEntity()).ToList(),
				ReminderIntervalDays = model.ReminderIntervalDays,
			};
		}
	}
}