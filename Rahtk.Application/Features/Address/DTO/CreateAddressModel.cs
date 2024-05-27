namespace Rahtk.Application.Features.Address.DTO
{
	public class CreateAddressModel
	{
        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}