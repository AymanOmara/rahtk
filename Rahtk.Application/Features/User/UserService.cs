using Rahtk.Application.Features.User.DTO;
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

        public async Task<BaseResponse<bool>> ChangePassword(string newPassword, string currentPassword, string userId)
        {
            var result = await _unitOfWork.Users.ChangePassword(newPassword: newPassword, currentPassword, userId: userId);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            return new BaseResponse<bool>
            {
                message = result.Value,
                success = true,
                statusCode = 200,
                data = true,
            };
        }

        public async Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration)
        {

            var result = await _unitOfWork.Users.CreateUser(registration);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    message = result.Error.Message,
                    statusCode = 400
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    message = result.Value,
                    statusCode = 200,
                    success = true,
                    data = true
                };
            }
        }

        public async Task<BaseResponse<bool>> EmailVerification(string email)
        {
            var result = await _unitOfWork.Users.EmailVerification(email);

            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    message = result.Value,
                    statusCode = 200,
                    success = true,
                    data = true
                };
            }
        }

        public async Task<BaseResponse<bool>> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            var result = await _unitOfWork.Users.ForgetPassword(forgetPassword);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    message = result.Value,
                    statusCode = 200,
                    success = true,
                    data = true,
                };
            }

        }

        public async Task<BaseResponse<ICollection<NotificationModel>>> GetNotifications(string email)
        {
            var result = await _unitOfWork.Users.GetNotifications(email);
            return new BaseResponse<ICollection<NotificationModel>> { statusCode = 200, success = true, data = result.Select(e => e.ToModel()).ToList() };
        }

        public async Task<BaseResponse<ProfileEntity>> GetProfileInfo(string email)
        {
            var result = await _unitOfWork.Users.GetProfileInfo(email);
            return new BaseResponse<ProfileEntity> { data = result, statusCode = 200, success = true };
        }

        public async Task<BaseResponse<TokenModel>> Login(LoginDTO login)
        {
            var result = await _unitOfWork.Users.Login(login);

            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>()
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<TokenModel>()
                {
                    message = _languageService.Getkey("user_logged_in_successfully").Value,
                    statusCode = 200,
                    success = true,
                    data = result.Value,
                };
            }
        }

        public async Task<BaseResponse<bool>> Logout(string email)
        {
            await _unitOfWork.Users.Logout(email);
            return new BaseResponse<bool>
            {
                data = true,
                statusCode = 200,
                success = true,
                message = _languageService.Getkey("logut_successfully").Value
            };
        }

        public async Task<BaseResponse<TokenModel>> RefreshToken(TokenModel token)
        {
            var result = await _unitOfWork.Users.RefreshToken(token);
            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            return new BaseResponse<TokenModel>
            {
                data = result.Value,
                statusCode = 200,
                success = true,
                message = _languageService.Getkey("token_refreshed_successfully").Value,
            };
        }

        public async Task<BaseResponse<TokenModel>> SocailLogin(LoginDTO login)
        {
            var result = await _unitOfWork.Users.SocailLogin(login);

            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>()
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<TokenModel>()
                {
                    message = _languageService.Getkey("user_logged_in_successfully").Value,
                    statusCode = 200,
                    success = true,
                    data = result.Value,
                };
            }
        }

        public async Task<BaseResponse<bool>> VerifyOTP(string otp, string email)
        {
            var result = await _unitOfWork.Users.VerifyOTP(otp: otp, email: email);

            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    message = result.Error.Message,
                    statusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    message = result.Value,
                    statusCode = 200,
                    success = true,
                    data = true
                };
            }
        }
    }
}

