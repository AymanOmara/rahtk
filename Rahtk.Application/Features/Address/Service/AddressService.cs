using Rahtk.Application.Features.Address.DTO;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Address.Service
{
    public class AddressService(IUnitOfWork unitOfWork, LanguageService languageService) : IAddressService
    {
        public async Task<BaseResponse<AddressEntity>> CreateAddress(CreateAddressModel model, string userEmail)
        {
            var result = await unitOfWork.Address.Create(model.ToEntity(), userEmail);
            await unitOfWork.SaveChangesAsync();
            return new BaseResponse<AddressEntity>
            {
                Data = result, Message = languageService.GetKey("address_created_successfully").Value, StatusCode = 200,
                Success = true
            };
        }

        public async Task<BaseResponse<ICollection<AddressEntity>>> GetAddresses(string userEmail)
        {
            var result = await unitOfWork.Address.GetAddresses(userEmail);
            return new BaseResponse<ICollection<AddressEntity>>
                { Data = result, Message = "", StatusCode = 200, Success = true };
        }
    }
}