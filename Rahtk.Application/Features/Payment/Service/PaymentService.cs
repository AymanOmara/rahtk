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

            return new BaseResponse<PaymentOptionEntity> { data = result, message = _languageService.Getkey("payment_option_added_successfully").Value, statusCode = 200, success = true };
        }

        public async Task<BaseResponse<ICollection<PaymentOptionEntity>>> GetPaymentOptions(string userId)
        {
            var result = await _unitOfWork.Payment.GetPaymentOptions(userId);
            return new BaseResponse<ICollection<PaymentOptionEntity>> { data = result, statusCode = 200, success = true };
        }
    }
}

