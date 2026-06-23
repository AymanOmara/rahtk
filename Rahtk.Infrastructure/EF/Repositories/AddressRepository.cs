using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Address;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class AddressRepository(RahtkContext context, UserManager<RahtkUser> userManager) : IAddressRepository
    {
        public async Task<ICollection<AddressEntity>> GetAddresses(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var result = await context.Addresses.AsNoTracking().Where(add => add.RahtkUserId == user.Id).ToListAsync();
            return result;
        }

        public async Task<AddressEntity> Create(AddressEntity address, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            address.RahtkUserId = user.Id;
            await context.Addresses.AddAsync(address);
            return address;
        }
    }
}