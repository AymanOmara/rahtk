using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
	public class PaymentRepository(RahtkContext context, UserManager<RahtkUser> userManager) : IPaymentRepository
    {
        public async Task<ICollection<PaymentOptionEntity>> GetPaymentOptions(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            var result = await context.PaymentOptions.AsNoTracking().Where(add => add.RahtkUserId == user.Id).ToListAsync();
            return result;
        }

        public async Task<PaymentOptionEntity> CreatePaymentOption(string userEmail, PaymentOptionEntity paymentOption)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            paymentOption.RahtkUserId = user.Id;
            await context.PaymentOptions.AddAsync(paymentOption);
            return paymentOption;
        }
    }
}

