using System.Security.Claims;
using Rahtk.Domain.Features.User;

namespace Rahtk.Contracts.Common
{
    public interface ITokenService
    {
        Task<TokenModel> CreateJwtToken(RahtkUser user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
