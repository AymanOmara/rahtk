﻿namespace Rahtk.Domain.Features.User
{
	public class TokenModel
	{
		public string AccessToken { get; set; } = string.Empty;

		public string RefreshToken { get; set; } = string.Empty;
	}
}

