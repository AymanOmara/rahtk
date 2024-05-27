using System.ComponentModel.DataAnnotations.Schema;

namespace Rahtk.Domain.Features.User
{
    public class AddressEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        [ForeignKey("RahtkUserId")]
        public string RahtkUserId { get; set; }
    }
}