using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.User;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class UserRepository(
        RahtkContext context,
        UserManager<RahtkUser> userManager,
        SignInManager<RahtkUser> signInManager,
        LanguageService localization,
        IUserNotifier userNotifier,
        ITokenService tokenService
    ) : IUserRepository
    {
        public async Task<ProfileEntity> GetProfileInfo(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return new ProfileEntity
            {
                Email = user.Email, UserName = user.FirstName + user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<Result<string, Exception>> CreateUser(RegistrationDTO registration)
        {
            var isUserExict = await IsUserExist(registration.Email);
            if (isUserExict)
            {
                return new Exception(localization.GetKey("user_already_exists").Value);
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

            var result = await userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return new Exception(string.Join(",", result.Errors.ToList()));
            }

            return localization.GetKey("user_created_success_fully").Value;
        }

        public async Task<bool> IsUserExist(string email)
        {
            var isEmailExist = await userManager.FindByEmailAsync(email);
            return isEmailExist != null;
        }

        public async Task<Result<TokenModel, Exception>> Login(LoginDTO d)
        {
            var user = await userManager.FindByEmailAsync(d.Email);
            if (user == null)
            {
                return new Exception(localization.GetKey("invalid_user_name").Value);
            }

            var result = await userManager.CheckPasswordAsync(user, d.Password);
            if (!result)
            {
                return new Exception(localization.GetKey("invalid_password").Value);
            }

            var token = await tokenService.CreateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.FcmToken = d.FcmToken;
            await userManager.UpdateAsync(user);
            return token;
        }

        private static string RefreshTokenGeneration()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public async Task<Result<TokenModel, Exception>> SocialLogin(LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                RahtkUser rahtkUser = new()
                {
                    Email = login.Email,
                    UserName = login.Email,
                    EmailConfirmed = true,
                };
                var userCreationResult = await userManager.CreateAsync(rahtkUser);
                if (!userCreationResult.Succeeded)
                {
                    return new Exception(localization.GetKey("error_try_again_later").Value);
                }

                user = rahtkUser;
            }

            var token = await tokenService.CreateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);
            return token;
        }

        public async Task<Result<string, Exception>> EmailVerification(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var isUserExist = user != null;
            if (!isUserExist)
            {
                return new Exception(localization.GetKey("email_not_exists").Value);
            }

            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);
            user.VerificationToken = randomNumber.ToString();
            await userManager.UpdateAsync(user);

            await userNotifier.Notify(email,
                $"{localization.GetKey("your_email_verification_is").Value} {randomNumber}");
            return localization.GetKey("verification_email_sent").Value;
        }

        public async Task<Result<string, Exception>> VerifyOTP(string otp, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var isUserExist = user != null;
            if (!isUserExist)
            {
                return new Exception(localization.GetKey("email_not_exists").Value);
            }

            if (otp != user.VerificationToken)
            {
                return new Exception(localization.GetKey("wrong_otp").Value);
            }

            return localization.GetKey("correct_otp").Value;
        }

        public async Task<Result<string, Exception>> ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            var user = await userManager.FindByEmailAsync(forgetPassword.Email);
            var isUserExist = user != null;
            if (!isUserExist)
            {
                return new Exception(localization.GetKey("email_not_exists").Value);
            }

            if (user.VerificationToken != forgetPassword.OTP)
            {
                return new Exception(localization.GetKey("wrong_otp").Value);
            }

            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, forgetPassword.Password);
            await userManager.UpdateAsync(user);
            return localization.GetKey("password_changed_successfully").Value;
        }

        public async Task<Result<string, Exception>> ChangePassword(string newPassword, string currentPassword,
            string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            try
            {
                await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception)
            {
                return new Exception(localization.GetKey("error_change_password").Value);
            }

            return localization.GetKey("password_changed_successfully").Value;
        }

        public async Task<Result<TokenModel, Exception>> RefreshToken(TokenModel oldToken)
        {
            try
            {
                var principal = tokenService.GetPrincipalFromExpiredToken(oldToken.AccessToken);
                var email = principal.FindFirst(ClaimTypes.Email)?.Value
                            ?? principal.FindFirst(JwtRegisteredClaimNames.Email)
                                ?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return new Exception(localization.GetKey("user_not_found").Value);
                }

                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new Exception(localization.GetKey("user_not_found").Value);
                }

                if (user.RefreshToken != oldToken.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    return new Exception(localization.GetKey("invalid_token").Value);
                }

                var token = await tokenService.CreateJwtToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await userManager.UpdateAsync(user);
                return token;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<ICollection<NotificationEntity>> GetNotifications(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            var result = await context.Notifications.Where(n => n.UserId == user.Id).ToListAsync();

            return result;
        }

        public async Task Logout(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            user.FcmToken = "";
            await userManager.UpdateAsync(user);
            await signInManager.SignOutAsync();
        }

        public async Task RegisterFCM(string email, string fcmToken)
        {
            var user = await userManager.FindByEmailAsync(email);
            user.FcmToken = fcmToken;
            await userManager.UpdateAsync(user);
        }
    }
}