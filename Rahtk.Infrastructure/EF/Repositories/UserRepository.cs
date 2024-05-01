﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
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
        public UserRepository(RahtkContext context, UserManager<RahtkUser> userManager, SignInManager<RahtkUser> signInManager, LanguageService localization, IConfiguration configuration, IUserNotifier userNotifier)
		{
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _localization = localization;
            _configuration = configuration;
            _userNotifier = userNotifier;

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
            var token = await CreateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            await _userManager.UpdateAsync(user);
            return token;
        }

        private string RefreshTokenGeneration()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<TokenModel> CreateJwtToken(RahtkUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"].ToString()),
                    new Claim("uid", user.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = signingCredentials
            };

            tokenDes.Subject.AddClaims(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return new TokenModel() { AccessToken = jwtToken, RefreshToken = RefreshTokenGeneration() };
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
                if (!userCreationResult.Succeeded) {
                    return new Exception(_localization.Getkey("error_try_agian_later").Value);
                }
                user = rahtkUser;
            }
            var token = await CreateJwtToken(user);
            user.RefreshToken = token.RefreshToken;
            await _userManager.UpdateAsync(user);
            return token;
        }

        public async Task<Result<String, Exception>> EmailVerification(String email) {
            var user = await _userManager.FindByEmailAsync(email);
            var isUserExist = user != null;
            if (!isUserExist) {
                return new Exception(_localization.Getkey("email_not_exists").Value);
            }
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);
            user.VerificationToken = randomNumber.ToString();
            await _userManager.UpdateAsync(user);
            
            await _userNotifier.Notify(email,$"{_localization.Getkey("your_email_verification_is").Value} {randomNumber}");
            return _localization.Getkey("verfification_email_sent").Value;
        }
    }
}
