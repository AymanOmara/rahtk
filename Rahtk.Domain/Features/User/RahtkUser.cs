using Microsoft.AspNetCore.Identity;
namespace Rahtk.Domain.Features.User
{
	public class RahtkUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;

        public string VerificationToken { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}

