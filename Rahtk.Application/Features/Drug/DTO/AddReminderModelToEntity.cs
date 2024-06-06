using Rahtk.Domain.Features.Pharmacy;

namespace Rahtk.Application.Features.Drug.DTO
{
    public static class AddReminderModelToEntity
    {
        public static ReminderEntity ToEntity(this AddReminderModel model)
        {
            return new ReminderEntity
            {
                DrugId = model.DrugId,
                ReminderDate = DateTime.Now,
                ReminderIntervalDays = model.ReminderIntervalDays,
            };
        }
    }
}