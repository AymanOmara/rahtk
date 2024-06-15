using Rahtk.Application.Features.product.DTO;
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
            return new BaseResponse<ReadReminderModel>
            {
                data = result.ToModel(),
                statusCode = 200,
                success = true,
                message = _languageService.Getkey("reminder_added_successfully").Value
            };

        }

        public async Task<BaseResponse<ReadReminderModel>> UpdateReminder(UpdateReminderModel reminderModel, string email)
        {
            var result = await _unitofWork.Reminder.UpdateReminder(reminderModel.ToEntity(),email);
            return new BaseResponse<ReadReminderModel>
            {
                data = result.ToModel(),
                statusCode = 200,
                success = true,
                message = _languageService.Getkey("reminder_updated_successfully").Value,
            };
        }

        public async Task<BaseResponse<bool>> DeleteReminder(UpdateReminderModel reminderModel, string email)
        {
            await _unitofWork.Reminder.DeleteReminder(reminderModel.ToEntity(), email);
            return new BaseResponse<bool>
            {
                data = true,
                statusCode = 200,
                success = true,
                message = _languageService.Getkey("reminder_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadReminderModel>>> GetAllReminders(string email)
        {
            var result = await _unitofWork.Reminder.GetAllReminders(email);

            return new BaseResponse<ICollection<ReadReminderModel>>
            {
                data = result.Select(re => re.ToModel()).ToList(),
                statusCode = 200,
                success = true,
            };
        }

    }
}