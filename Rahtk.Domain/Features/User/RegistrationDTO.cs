using System.ComponentModel.DataAnnotations;

namespace Rahtk.Domain.Features.User
{
    public record RegistrationDTO(
        [property: Required] string Email,
        [property: Required] string Password,
        [property: Required] string FirstName,
        [property: Required] string LastName,
        [property: Required] string PhoneNumber
    );
}

