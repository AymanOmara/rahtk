using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Features.Address;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly RahtkContext _context;
        public readonly UserManager<RahtkUser> _userManager;
        public AddressRepository(RahtkContext context, UserManager<RahtkUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ICollection<AddressEntity>> GetAddresses(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _context.Addresses.Where(add => add.RahtkUserId == user.Id).ToListAsync();
            return result;
        }

        public async Task<AddressEntity> Create(AddressEntity address, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            address.RahtkUserId = user.Id;
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }
    }
}

