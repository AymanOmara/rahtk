using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features
{
	public interface IUserService
	{
        Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration);
        
        Task<BaseResponse<TokenModel>> Login(LoginDTO login);

        Task<BaseResponse<TokenModel>> RefreshToken(TokenModel token);
        
        Task<BaseResponse<TokenModel>> SocailLogin(LoginDTO login);

        Task<BaseResponse<bool>> EmailVerification(string email);

        Task<BaseResponse<bool>> VerifyOTP(string otp, string email);

        Task<BaseResponse<bool>> ForgetPassword(ForgetPasswordModel forgetPassword);

        Task<BaseResponse<bool>> ChangePassword(string newPassword,string currentPassword,string email);

        Task<BaseResponse<ProfileEntity>> GetProfileInfo(string email);
    }
}

