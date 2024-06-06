using Rahtk.Application.Features.Drug.DTO;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Pharmacy;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Drug.Service
{
    public class DrugService : IDrugService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public DrugService(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;

            _languageService = languageService;
        }

        public async Task<BaseResponse<DrugEntity>> AddDrug(AddDrugModel drugModel)
        {
            var result = await _unitOfWork.Drug.AddDrug(new DrugEntity { Name = drugModel.Name, Price = drugModel.Price,DiscountPercentage = drugModel.DiscountPercentage}, drugModel.Image);
            return new BaseResponse<DrugEntity> { data = result, statusCode = 200, success = true };
        }

        public async Task<BaseResponse<bool>> AddReminder(AddReminderModel reminder, string userEmail)
        {
            var result = await _unitOfWork.Drug.AddReminder(reminder.ToEntity(),userEmail);
            return new BaseResponse<bool> { data = true, statusCode = 200, success = true, message = _languageService.Getkey("reminder_added_successfully").Value };
        }

        public async Task<BaseResponse<ICollection<DrugEntity>>> GetDrugs()
        {
            var result = await _unitOfWork.Drug.GetDrugs();
            return new BaseResponse<ICollection<DrugEntity>> { data = result, statusCode = 200, success = true };
        }
    }
}

