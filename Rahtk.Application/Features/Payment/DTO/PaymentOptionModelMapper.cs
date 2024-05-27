using System;
using Rahtk.Domain.Features.User;

namespace Rahtk.Application.Features.Payment.DTO
{
    public static class PaymentOptionModelMapper
    {
        public static PaymentOptionEntity ToEntity(this CreatePaymentOptionModel model)
        {
            return new PaymentOptionEntity
            {
                CardHolder = model.CardHolder,
                CardNumber = model.CardNumber,
                CVV = model.CVV,
                ExpirationDate = model.ExpirationDate,
            };
        }
    }
}

