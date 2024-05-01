using Rahtk.Domain.Features.User;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features
{
	public interface IUserService
	{
        Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration);
        
        Task<BaseResponse<TokenModel>> Login(LoginDTO login);

        Task<BaseResponse<TokenModel>> SocailLogin(LoginDTO login);

        Task<BaseResponse<bool>> EmailVerification(string email);
    }
}

