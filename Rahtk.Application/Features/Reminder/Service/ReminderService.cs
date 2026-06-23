using Rahtk.Application.Features.Product.DTO;
using Rahtk.Application.Features.Product.mappers;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Application.Features.Reminder.Mapper;
using Rahtk.Contracts.Common;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Reminder.Service
{
    public class ReminderService(IUnitOfWork unitOfWork, LanguageService languageService) : IReminderService
    {
        public async Task<BaseResponse<ReadReminderModel>> AddReminder(AddReminderModel reminderModel, string email)
        {
            var result = await unitOfWork.Reminder.AddReminder(reminderModel.ToEntity(),email);
            unitOfWork.RegisterPostCommitAction(() =>
            {
                unitOfWork.Reminder.ScheduleReminder(result);
                return Task.CompletedTask;
            });
            await unitOfWork.SaveChangesAsync();
            return new BaseResponse<ReadReminderModel>
            {
                Data = result.ToModel(),
                StatusCode = 200,
                Success = true,
                Message = languageService.GetKey("reminder_added_successfully").Value
            };

        }

        public async Task<BaseResponse<ReadReminderModel>> UpdateReminder(UpdateReminderModel reminderModel, string email)
        {
            var result = await unitOfWork.Reminder.UpdateReminder(reminderModel.ToEntity(),email);
            unitOfWork.RegisterPostCommitAction(() =>
            {
                unitOfWork.Reminder.ScheduleReminder(result);
                return Task.CompletedTask;
            });
            await unitOfWork.SaveChangesAsync();
            return new BaseResponse<ReadReminderModel>
            {
                Data = result.ToModel(),
                StatusCode = 200,
                Success = true,
                Message = languageService.GetKey("reminder_updated_successfully").Value,
            };
        }

        public async Task<BaseResponse<bool>> DeleteReminder(UpdateReminderModel reminderModel, string email)
        {
            await unitOfWork.Reminder.DeleteReminder(reminderModel.ToEntity(), email);
            await unitOfWork.SaveChangesAsync();
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = 200,
                Success = true,
                Message = languageService.GetKey("reminder_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadReminderModel>>> GetAllReminders(string email)
        {
            var result = await unitOfWork.Reminder.GetAllReminders(email);

            return new BaseResponse<ICollection<ReadReminderModel>>
            {
                Data = result.Select(re => re.ToModel()).ToList(),
                StatusCode = 200,
                Success = true,
            };
        }

    }
}