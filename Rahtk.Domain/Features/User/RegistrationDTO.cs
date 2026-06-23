using System.ComponentModel.DataAnnotations;

namespace Rahtk.Domain.Features.User
{
    public record RegistrationDTO(
        [Required] string Email,
        [Required] string Password,
        [Required] string FirstName,
        [Required] string LastName,
        [Required] string PhoneNumber
    );
}

