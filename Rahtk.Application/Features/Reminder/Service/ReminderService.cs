using Rahtk.Application.Features.Product.DTO;
using Rahtk.Application.Features.Reminder.DTO;
using Rahtk.Application.Features.Reminder.Mapper;
using Rahtk.Contracts.Common;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Reminder.Service
{
    public class ReminderService : IReminderService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly LanguageService _languageService;
        public ReminderService(IUnitOfWork unitofWork, LanguageService languageService)
        {
            _unitofWork = unitofWork;
            _languageService = languageService;
        }
        public async Task<BaseResponse<ReadReminderModel>> AddReminder(AddReminderModel reminderModel, string email)
        {
            var result = await _unitofWork.Reminder.AddReminder(reminderModel.ToEntity(),email);
            await _unitofWork.SaveChangesAsync();
            _unitofWork.Reminder.ScheduleReminder(result);
            return new BaseResponse<ReadReminderModel>
            {
                Data = result.ToModel(),
                StatusCode = 200,
                Success = true,
                Message = _languageService.Getkey("reminder_added_successfully").Value
            };

        }

        public async Task<BaseResponse<ReadReminderModel>> UpdateReminder(UpdateReminderModel reminderModel, string email)
        {
            var result = await _unitofWork.Reminder.UpdateReminder(reminderModel.ToEntity(),email);
            await _unitofWork.SaveChangesAsync();
            _unitofWork.Reminder.ScheduleReminder(result);
            return new BaseResponse<ReadReminderModel>
            {
                Data = result.ToModel(),
                StatusCode = 200,
                Success = true,
                Message = _languageService.Getkey("reminder_updated_successfully").Value,
            };
        }

        public async Task<BaseResponse<bool>> DeleteReminder(UpdateReminderModel reminderModel, string email)
        {
            await _unitofWork.Reminder.DeleteReminder(reminderModel.ToEntity(), email);
            await _unitofWork.SaveChangesAsync();
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = 200,
                Success = true,
                Message = _languageService.Getkey("reminder_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadReminderModel>>> GetAllReminders(string email)
        {
            var result = await _unitofWork.Reminder.GetAllReminders(email);

            return new BaseResponse<ICollection<ReadReminderModel>>
            {
                Data = result.Select(re => re.ToModel()).ToList(),
                StatusCode = 200,
                Success = true,
            };
        }

    }
}