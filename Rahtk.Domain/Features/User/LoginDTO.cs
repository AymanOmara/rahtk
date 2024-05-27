using System.ComponentModel.DataAnnotations;

namespace Rahtk.Domain.Features.User
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string FcmToken { get; set; }
    }
}