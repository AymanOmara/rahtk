using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.User;
using Rahtk.Domain.Features.User;

namespace Rahtk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO dto)
        {
            var result = await userService.CreateUser(dto);
            return result.ToResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await userService.Login(dto);
            return result.ToResult();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel token)
        {
            var result = await userService.RefreshToken(token);
            return result.ToResult();
        }

        [HttpPost("social-login")]
        public async Task<IActionResult> SocialLogin([FromBody] LoginDTO dto)
        {
            var result = await userService.SocialLogin(dto);
            return result.ToResult();
        }

        [HttpPost("email-verification")]
        public async Task<IActionResult> EmailVerification(string email)
        {
            var result = await userService.EmailVerification(email);
            return result.ToResult();
        }

        [HttpGet("verify-otp")]
        public async Task<IActionResult> VerifyOTP(string email, string otp)
        {
            var result = await userService.VerifyOTP(email: email, otp: otp);
            return result.ToResult();
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordModel forgetPassword)
        {
            var result = await userService.ForgetPassword(forgetPassword);
            return result.ToResult();
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(string newPassword, string currentPassword)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await userService.ChangePassword(newPassword, currentPassword, email ?? "");
            return result.ToResult();
        }

        [Authorize]
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfileInfo()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await userService.GetProfileInfo(email);
            return result.ToResult();
        }

        [Authorize]
        [HttpGet("get-notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await userService.GetNotifications(email);
            return result.ToResult();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await userService.Logout(email);

            return result.ToResult();
        }

        [Authorize]
        [HttpPost("register-fcm-token")]
        public async Task<IActionResult> RegisterFCM(string fcmToken) {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await userService.RegisterFCM(email, fcmToken);
            return result.ToResult();
        }
        
    }
}