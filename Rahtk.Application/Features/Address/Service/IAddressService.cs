using Rahtk.Application.Features.Address.DTO;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Address.Service
{
	public interface IAddressService
	{
        Task<BaseResponse<AddressEntity>> CreateAddress(CreateAddressModel model,string userEmail);

        Task<BaseResponse<ICollection<AddressEntity>>> GetAddresses(string userEmail);
    }
}