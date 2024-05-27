using Rahtk.Domain.Features.User;

namespace Rahtk.Application.Features.Address.DTO
{
    public static class CreateAddressModelMapper
    {
        public static AddressEntity ToEntity(this CreateAddressModel model)
        {
            return new AddressEntity
            {
                City = model.City,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                State = model.State,
                Street = model.Street,
                ZipCode = model.ZipCode,
            };
        }
    }
}

