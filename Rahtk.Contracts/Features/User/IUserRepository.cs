using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Contracts.Features.User
{
	public interface IUserRepository
	{
        Task<Result<string, Exception>> CreateUser(RegistrationDTO registration);

        Task<Result<TokenModel, Exception>> Login(LoginDTO login);

        Task<Result<TokenModel, Exception>> SocailLogin(LoginDTO login);

        Task<Result<String, Exception>> EmailVerification(String email);
    }
}

