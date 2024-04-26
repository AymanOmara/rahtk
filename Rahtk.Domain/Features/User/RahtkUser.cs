using Microsoft.AspNetCore.Identity;
namespace Rahtk.Domain.Features.User
{
	public class RahtkUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}

