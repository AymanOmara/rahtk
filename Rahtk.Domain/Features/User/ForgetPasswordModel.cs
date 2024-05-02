namespace Rahtk.Domain.Features.User
{
	public class ForgetPasswordModel
	{
		public string OTP { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;
	}
}

