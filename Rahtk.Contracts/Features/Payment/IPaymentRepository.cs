using System;
using Rahtk.Domain.Features.User;

namespace Rahtk.Contracts.Features.Payment
{
	public interface IPaymentRepository
	{
		Task<ICollection<PaymentOptionEntity>> GetPaymentOptions(string userEmail);

        Task<PaymentOptionEntity> CreatePaymentOption(string userEmail, PaymentOptionEntity paymentOption);
    }
}

