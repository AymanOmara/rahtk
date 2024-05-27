using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
	public class PaymentRepository: IPaymentRepository
    {
        private readonly RahtkContext _context;
        private readonly UserManager<RahtkUser> _userManager;
        public PaymentRepository(RahtkContext context, UserManager<RahtkUser> userManager)
		{
            _context = context;
            _userManager = userManager;
		}

        public async Task<ICollection<PaymentOptionEntity>> GetPaymentOptions(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _context.PaymentOptions.Where(add => add.RahtkUserId == user.Id).ToListAsync();
            return result;
        }

        public async Task<PaymentOptionEntity> CreatePaymentOption(string userEmail, PaymentOptionEntity paymentOption)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            paymentOption.RahtkUserId = user.Id;
            await _context.PaymentOptions.AddAsync(paymentOption);
            await _context.SaveChangesAsync();
            return paymentOption;
        }
    }
}

