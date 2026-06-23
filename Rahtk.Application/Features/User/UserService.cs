using FluentValidation;
using Rahtk.Application.Features.User.DTO;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.User;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.User
{
    public class UserService(
        IUnitOfWork unitOfWork, 
        LanguageService languageService,
        IValidator<RegistrationDTO> registrationValidator,
        IValidator<LoginDTO> loginValidator) : IUserService
    {
        public async Task<BaseResponse<bool>> ChangePassword(string newPassword, string currentPassword, string userId)
        {
            var result = await unitOfWork.Users.ChangePassword(newPassword: newPassword, currentPassword, userId: userId);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            return new BaseResponse<bool>
            {
                Message = result.Value,
                Success = true,
                StatusCode = 200,
                Data = true,
            };
        }

        public async Task<BaseResponse<bool>> CreateUser(RegistrationDTO registration)
        {
            var validationResult = await registrationValidator.ValidateAsync(registration);
            if (!validationResult.IsValid)
            {
                return new BaseResponse<bool>()
                {
                    Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    StatusCode = 400
                };
            }

            var result = await unitOfWork.Users.CreateUser(registration);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Error.Message,
                    StatusCode = 400
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Value,
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
        }

        public async Task<BaseResponse<bool>> EmailVerification(string email)
        {
            var result = await unitOfWork.Users.EmailVerification(email);

            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Value,
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
        }

        public async Task<BaseResponse<bool>> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            var result = await unitOfWork.Users.ForgetPassword(forgetPassword);
            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Value,
                    StatusCode = 200,
                    Success = true,
                    Data = true,
                };
            }

        }

        public async Task<BaseResponse<ICollection<NotificationModel>>> GetNotifications(string email)
        {
            var result = await unitOfWork.Users.GetNotifications(email);
            return new BaseResponse<ICollection<NotificationModel>> { StatusCode = 200, Success = true, Data = result.Select(e => e.ToModel()).ToList() };
        }

        public async Task<BaseResponse<ProfileEntity>> GetProfileInfo(string email)
        {
            var result = await unitOfWork.Users.GetProfileInfo(email);
            return new BaseResponse<ProfileEntity> { Data = result, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<TokenModel>> Login(LoginDTO login)
        {
            var validationResult = await loginValidator.ValidateAsync(login);
            if (!validationResult.IsValid)
            {
                return new BaseResponse<TokenModel>()
                {
                    Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    StatusCode = 400,
                };
            }

            var result = await unitOfWork.Users.Login(login);

            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>()
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<TokenModel>()
                {
                    Message = languageService.GetKey("user_logged_in_successfully").Value,
                    StatusCode = 200,
                    Success = true,
                    Data = result.Value,
                };
            }
        }

        public async Task<BaseResponse<bool>> Logout(string email)
        {
            await unitOfWork.Users.Logout(email);
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = 200,
                Success = true,
                Message = languageService.GetKey("logout_successfully").Value
            };
        }

        public async Task<BaseResponse<TokenModel>> RefreshToken(TokenModel token)
        {
            var result = await unitOfWork.Users.RefreshToken(token);
            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            return new BaseResponse<TokenModel>
            {
                Data = result.Value,
                StatusCode = 200,
                Success = true,
                Message = languageService.GetKey("token_refreshed_successfully").Value,
            };
        }

        public async Task<BaseResponse<bool>> RegisterFCM(string email, string token)
        {
            await unitOfWork.Users.RegisterFCM(email, token);
            return new BaseResponse<bool> { Data = true, StatusCode = 200};
        }

        public async Task<BaseResponse<TokenModel>> SocialLogin(LoginDTO login)
        {
            var result = await unitOfWork.Users.SocialLogin(login);

            if (!result.IsOk)
            {
                return new BaseResponse<TokenModel>()
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<TokenModel>()
                {
                    Message = languageService.GetKey("user_logged_in_successfully").Value,
                    StatusCode = 200,
                    Success = true,
                    Data = result.Value,
                };
            }
        }

        public async Task<BaseResponse<bool>> VerifyOTP(string otp, string email)
        {
            var result = await unitOfWork.Users.VerifyOTP(otp: otp, email: email);

            if (!result.IsOk)
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Error.Message,
                    StatusCode = 400,
                };
            }
            else
            {
                return new BaseResponse<bool>()
                {
                    Message = result.Value,
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
        }
    }
}
