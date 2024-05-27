using Rahtk.Application.Features.Address.DTO;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Address.Service
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public AddressService(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;

            _languageService = languageService;
        }

        public async Task<BaseResponse<AddressEntity>> CreateAddress(CreateAddressModel model, string userEmail)
        {
            var result = await _unitOfWork.Address.Create(model.ToEntity(),userEmail);
            return new BaseResponse<AddressEntity> { data = result, message = _languageService.Getkey("address_created_successfully").Value, statusCode = 200 , success = true};
        }
        public async Task<BaseResponse<ICollection<AddressEntity>>> GetAddresses( string userEmail)
        {
            var result = await _unitOfWork.Address.GetAddresses(userEmail);
            return new BaseResponse<ICollection<AddressEntity>> { data = result, message = "", statusCode = 200, success = true };
        }
    }
}

