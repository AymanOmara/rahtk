using Rahtk.Application.Features.Payment.DTO;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Payment.Service
{
	public interface IPaymentService
	{
        Task<BaseResponse<ICollection<PaymentOptionEntity>>> GetPaymentOptions(string userId);

        Task<BaseResponse<PaymentOptionEntity>> CreatePayment(CreatePaymentOptionModel paymentOptionModel, string userId);
    }
}

