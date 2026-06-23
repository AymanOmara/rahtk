using System.ComponentModel.DataAnnotations;

namespace Rahtk.Domain.Features.User
{
    public record LoginDTO(
        [property: Required] string Email,
        string Password,
        string FcmToken
    );
}