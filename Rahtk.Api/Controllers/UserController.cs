using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features;
using Rahtk.Domain.Features.User;

namespace Rahtk.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO dto)
        {
            var result = await _userService.CreateUser(dto);
            return result.toResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _userService.Login(dto);
            return result.toResult();
        }

        [HttpPost("socailLogin")]
        public async Task<IActionResult> SocailLogin([FromBody] LoginDTO dto)
        {
            var result = await _userService.SocailLogin(dto);
            return result.toResult();
        }

        [HttpPost("emailVerification")]
        public async Task<IActionResult> EmailVerification([FromBody] string email)
        {
            var result = await _userService.EmailVerification(email);
            return result.toResult();
        }

        [HttpGet("verify-otp")]
        public async Task<IActionResult> VerifyOTP(string email, string otp) {
            var result = await _userService.VerifyOTP(email:email,otp:otp);
            return result.toResult();
        }

        [HttpPost("forget-passwprd")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordModel forgetPassword) {
            var result = await _userService.ForgetPassword(forgetPassword);
            return result.toResult();
        }
    }
}

