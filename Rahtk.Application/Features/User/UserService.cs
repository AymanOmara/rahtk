using FluentValidation;
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
        private readonly IValidator<RegistrationDTO> _registrationValidator;
        private readonly IValidator<LoginDTO> _loginValidator;

        public UserService(
            IUnitOfWork unitOfWork, 
            LanguageService languageService,
            IValidator<RegistrationDTO> registrationValidator,
            IValidator<LoginDTO> loginValidator)
        {
            _unitOfWork = unitOfWork;
            _languageService = languageService;
            _registrationValidator = registrationValidator;
            _loginValidator = loginValidator;
        }

        public async Task<BaseResponse<bool>> ChangePassword(string newPassword, string currentPassword, string userId)
        {
            var result = await _unitOfWork.Users.ChangePassword(newPassword: newPassword, currentPassword, userId: userId);
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
            var validationResult = await _registrationValidator.ValidateAsync(registration);
            if (!validationResult.IsValid)
            {
                return new BaseResponse<bool>()
                {
                    Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    StatusCode = 400
                };
            }

            var result = await _unitOfWork.Users.CreateUser(registration);
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
            var result = await _unitOfWork.Users.EmailVerification(email);

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
            var result = await _unitOfWork.Users.ForgetPassword(forgetPassword);
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
            var result = await _unitOfWork.Users.GetNotifications(email);
            return new BaseResponse<ICollection<NotificationModel>> { StatusCode = 200, Success = true, Data = result.Select(e => e.ToModel()).ToList() };
        }

        public async Task<BaseResponse<ProfileEntity>> GetProfileInfo(string email)
        {
            var result = await _unitOfWork.Users.GetProfileInfo(email);
            return new BaseResponse<ProfileEntity> { Data = result, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<TokenModel>> Login(LoginDTO login)
        {
            var validationResult = await _loginValidator.ValidateAsync(login);
            if (!validationResult.IsValid)
            {
                return new BaseResponse<TokenModel>()
                {
                    Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    StatusCode = 400,
                };
            }

            var result = await _unitOfWork.Users.Login(login);

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
                    Message = _languageService.Getkey("user_logged_in_successfully").Value,
                    StatusCode = 200,
                    Success = true,
                    Data = result.Value,
                };
            }
        }

        public async Task<BaseResponse<bool>> Logout(string email)
        {
            await _unitOfWork.Users.Logout(email);
            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = 200,
                Success = true,
                Message = _languageService.Getkey("logut_successfully").Value
            };
        }

        public async Task<BaseResponse<TokenModel>> RefreshToken(TokenModel token)
        {
            var result = await _unitOfWork.Users.RefreshToken(token);
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
                Message = _languageService.Getkey("token_refreshed_successfully").Value,
            };
        }

        public async Task<BaseResponse<bool>> RegisterFCM(string email, string token)
        {
            await _unitOfWork.Users.RegisterFCM(email, token);
            return new BaseResponse<bool> { Data = true, StatusCode = 200};
        }

        public async Task<BaseResponse<TokenModel>> SocailLogin(LoginDTO login)
        {
            var result = await _unitOfWork.Users.SocailLogin(login);

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
                    Message = _languageService.Getkey("user_logged_in_successfully").Value,
                    StatusCode = 200,
                    Success = true,
                    Data = result.Value,
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

