using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Contracts.Features.User
{
	public interface IUserRepository
	{
        Task<ProfileEntity> GetProfileInfo(string email);

        Task<Result<string, Exception>> CreateUser(RegistrationDTO registration);

        Task<Result<TokenModel, Exception>> Login(LoginDTO login);

        Task<Result<TokenModel, Exception>> RefreshToken(TokenModel token);

        Task<Result<TokenModel, Exception>> SocailLogin(LoginDTO login);

        Task<Result<string, Exception>> EmailVerification(string email);

        Task<Result<string, Exception>> VerifyOTP(string otp, string email);

        Task<Result<string, Exception>> ForgetPassword(ForgetPasswordModel forgetPassword);

        Task<Result<string, Exception>> ChangePassword(string newPassword, string currentPassword, string userId);
    }
}

