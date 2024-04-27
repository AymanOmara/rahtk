using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Shared.Localization;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Rahtk.Api
{
    public static class Extensions
    {
        public static void SetUpApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetUpIdentity(services, configuration);
            SetUpLocalization(services, configuration);
        }
        private static void SetUpIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequireUppercase = false;
            });

            services.AddIdentity<RahtkUser, IdentityRole>()
            .AddEntityFrameworkStores<RahtkContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = configuration["JWT:Audience"],
        ValidIssuer = configuration["JWT:Issuer"],
        //RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            configuration["Jwt:Key"]
            ))
    };
});
        }

        private static void SetUpLocalization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                return factory.Create(nameof(SharedResource), assemblyName.Name);
            });

        }

        public static void UseLocalization(this WebApplication app) {
            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);
        }

    }
}

