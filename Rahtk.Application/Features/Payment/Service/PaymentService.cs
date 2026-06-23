using Rahtk.Application.Features.Payment.DTO;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Payment.Service
{
	public class PaymentService: IPaymentService
    {
		private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public PaymentService(IUnitOfWork unitOfWork, LanguageService languageService)
		{
			_unitOfWork = unitOfWork;
			_languageService = languageService;
		}

        public async Task<BaseResponse<PaymentOptionEntity>> CreatePayment(CreatePaymentOptionModel paymentOptionModel, string userId)
        {
            var result = await _unitOfWork.Payment.CreatePaymentOption(userId,paymentOptionModel.ToEntity());
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<PaymentOptionEntity> { Data = result, Message = _languageService.Getkey("payment_option_added_successfully").Value, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<ICollection<PaymentOptionEntity>>> GetPaymentOptions(string userId)
        {
            var result = await _unitOfWork.Payment.GetPaymentOptions(userId);
            return new BaseResponse<ICollection<PaymentOptionEntity>> { Data = result, StatusCode = 200, Success = true };
        }
    }
}

