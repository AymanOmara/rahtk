using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        public UserRepository(RahtkContext context, UserManager<RahtkUser> userManager, SignInManager<RahtkUser> signInManager, LanguageService localization, IConfiguration configuration)
		{
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _localization = localization;
            _configuration = configuration;

        }

        public async Task<Result<string,Exception>> CreateUser(RegistrationDTO registration)
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
    }
}

