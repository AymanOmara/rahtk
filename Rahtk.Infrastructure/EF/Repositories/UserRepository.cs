using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.User;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RahtkContext _context;
        public readonly UserManager<RahtkUser> _userManager;
        public readonly SignInManager<RahtkUser> _signInManager;
        private readonly LanguageService _localization;
        private readonly IConfiguration _configuration;
        private readonly IUserNotifier _userNotifier;
        private readonly ITokenService _tokenService;

        public UserRepository(
            RahtkContext context,
            UserManager<RahtkUser> userManager,
            SignInManager<RahtkUser> signInManager,
            LanguageService localization,
            IConfiguration configuration,
            IUserNotifier userNotifier,
            ITokenService tokenService
            )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _localization = localization;
            _configuration = configuration;
            _userNotifier = userNotifier;
            _tokenService = tokenService;

        }
        public async Task<ProfileEntity> GetProfileInfo(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            return new ProfileEntity { Email = user.Email, UserName = user.FirstName + user.LastName, PhoneNumber = user.PhoneNumber };
        }
        public async Task<Result<string, Exception>> CreateUser(RegistrationDTO registration)
        {
            var isUserExict = await IsUserExist(registration.Email);
            if (isUserExict)
            {
                return new Exception(_localization.Getkey("user_already_exists").Value);

            }
            RahtkUser user = new()
            {
                Email = registration.Email,
                UserName = registration.Email,
                EmailConfirmed = true,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                PhoneNumber = registration.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return new Exception(string.Join(",", result.Errors.ToList()));
            }

            return _localization.Getkey("user_created_success_fully").Value;
        }

        public async Task<bool> IsUserExist(string email)
        {
            var isEmailExist = await _userManager.FindByEmailAsync(email);
            return isEmailExist != null;
        }

        public async Task<Result<TokenModel, Exception>> Login(LoginDTO d)
        {
            var user = await _userManager.FindByEmailAsync(d.Email);
            if (user == null)
            {
                return new Exception(_localization.Getkey("invalid_user_name").Value);
            }
            var result = await _userManager.CheckPasswordAsync(user, d.Password);
            if (!result)
            {
                return new Exception(_localization.Getkey("invalid_password").Value);
            }
            var token = await _tokenService.CreateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.FcmToken = d.FcmToken;
            await _userManager.UpdateAsync(user);
            return token;
        }

        private static string RefreshTokenGeneration()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }



        public async Task<Result<TokenModel, Exception>> SocailLogin(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                RahtkUser rahtkUser = new()
                {
                    Email = login.Email,
                    UserName = login.Email,
                    EmailConfirmed = true,

                };
                var userCreationResult = await _userManager.CreateAsync(rahtkUser);
                if (!userCreationResult.Succeeded)
                {
                    return new Exception(_localization.Getkey("error_try_agian_later").Value);
                }
                user = rahtkUser;
            }
            var token = await _tokenService.CreateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);
            return token;
        }

        public async Task<Result<string, Exception>> EmailVerification(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var isUserExist = user != null;
            if (!isUserExist)
            {
                return new Exception(_localization.Getkey("email_not_exists").Value);
            }
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);
            user.VerificationToken = randomNumber.ToString();
            await _userManager.UpdateAsync(user);

            await _userNotifier.Notify(email, $"{_localization.Getkey("your_email_verification_is").Value} {randomNumber}");
            return _localization.Getkey("verfification_email_sent").Value;
        }

        public async Task<Result<string, Exception>> VerifyOTP(string otp, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var isUserExist = user != null;
            if (!isUserExist)
            {
                return new Exception(_localization.Getkey("email_not_exists").Value);
            }
            if (otp != user.VerificationToken)
            {
                return new Exception(_localization.Getkey("wrong_otp").Value);
            }
            return _localization.Getkey("correct_otp").Value;
        }

        public async Task<Result<string, Exception>> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgetPassword.Email);
            var isUserExist = user != null;
            if (!isUserExist)
            {
                return new Exception(_localization.Getkey("email_not_exists").Value);
            }

            if (user.VerificationToken != forgetPassword.OTP)
            {
                return new Exception(_localization.Getkey("wrong_otp").Value);
            }
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, forgetPassword.Password);
            await _userManager.UpdateAsync(user);
            return _localization.Getkey("password_changed_successfully").Value;
        }

        public async Task<Result<string, Exception>> ChangePassword(string newPassword, string currentPassword, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            try
            {
                await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception _)
            {
                return new Exception(_localization.Getkey("error_change_password").Value);
            }
            return _localization.Getkey("password_changed_successfully").Value;
        }

        public async Task<Result<TokenModel, Exception>> RefreshToken(TokenModel oldToken)
        {
            try
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(oldToken.AccessToken);
                var email = principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value 
                            ?? principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return new Exception(_localization.Getkey("user_not_found").Value);
                }

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new Exception(_localization.Getkey("user_not_found").Value);
                }

                if (user.RefreshToken != oldToken.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    return new Exception(_localization.Getkey("invalid_token").Value);
                }

                var token = await _tokenService.CreateJwtToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);
                return token;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<ICollection<NotificationEntity>> GetNotifications(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _context.Notifications.Where(n => n.UserId == user.Id).ToListAsync();

            return result;
        }

        public async Task Logout(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.FcmToken = "";
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
        }

        public async Task RegisterFCM(string email, string fcmToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.FcmToken = fcmToken;
            await _userManager.UpdateAsync(user);
        }
    }
}