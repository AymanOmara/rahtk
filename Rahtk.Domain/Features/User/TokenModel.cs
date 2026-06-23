namespace Rahtk.Domain.Features.User
{
	public record TokenModel(
		string AccessToken = "",
		string RefreshToken = ""
	);
}

