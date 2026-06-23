using System.ComponentModel.DataAnnotations;

namespace Rahtk.Domain.Features.User
{
    public record LoginDTO(
        [Required] string Email,
        string Password,
        string FcmToken
    );
}