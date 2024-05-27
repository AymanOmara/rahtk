using Rahtk.Domain.Features.User;

namespace Rahtk.Contracts.Features.Address
{
    public interface IAddressRepository
    {
        Task<ICollection<AddressEntity>> GetAddresses(string email);

        Task<AddressEntity> Create(AddressEntity address, string email);
    }
}