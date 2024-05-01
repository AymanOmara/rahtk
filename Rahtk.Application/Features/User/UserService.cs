using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.User
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public UserService(IUnitOfWork unitOfWork, LanguageService languageService)
		{
            
            _unitOfWork = unitOfWork;
            _languageService = languageService;
		}

        public async Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration)
        {
            
            var result = await _unitOfWork.Users.CreateUser(registration);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>() { message = result.Error.Message, statusCode = 400 };
            }
            else
            {
                return new BaseResponse<bool>() { message = result.Value, statusCode = 200, success = true, data = true };
            }
        }

        public async Task<BaseResponse<bool>> EmailVerification(string email)
        {
            var result = await _unitOfWork.Users.EmailVerification(email);

            if (!result.IsOk)
            {
                return new BaseResponse<bool>() { message = result.Error.Message, statusCode = 400 };
            }
            else
            {
                return new BaseResponse<bool>() { message = result.Value, statusCode = 200, success = true, data = true };
            }
        }

        public async Task<BaseResponse<TokenModel>> Login(LoginDTO login)
        {
            var result = await _unitOfWork.Users.Login(login);

            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>() { message = result.Error.Message, statusCode = 400 };
            }
            else
            {
                return new BaseResponse<TokenModel>() { message = _languageService.Getkey("user_logged_in_successfully").Value, statusCode = 200, success = true, data = result.Value, };
            }
        }

        public async Task<BaseResponse<TokenModel>> SocailLogin(LoginDTO login)
        {
            var result = await _unitOfWork.Users.SocailLogin(login);

            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>() { message = result.Error.Message, statusCode = 400 };
            }
            else
            {
                return new BaseResponse<TokenModel>() { message = _languageService.Getkey("user_logged_in_successfully").Value, statusCode = 200, success = true, data = result.Value, };
            }
        }
    }
}

