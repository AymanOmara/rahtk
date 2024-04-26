using System.ComponentModel.DataAnnotations;

namespace Rahtk.Domain.Features.User
{
	public class RegistrationDTO
	{
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

