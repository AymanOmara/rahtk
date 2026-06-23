using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Contracts.Features.Address;
using Rahtk.Contracts.Features.Order;
using Rahtk.Contracts.Features.Payment;
using Rahtk.Contracts.Features.Product.Prodcut;
using Rahtk.Contracts.Features.Reminder;
using Rahtk.Contracts.Features.User;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.Common;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Infrastructure.EF.Repositories;
using Rahtk.Infrastructure.EF.Services;
using System.Text;

namespace Rahtk.Infrastructure.EF
{
    public static class Extensions
    {

        public static void RegisterInfrastructureDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            AddSQL(services, configuration);
            SetUpIdentity(services, configuration);
            services.AddScoped<IUserNotifier, UserNotifier>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddSingleton<IFileService, FileService>();

            services.AddSingleton<INotificationSender, NotificationSender>();
            if (FirebaseApp.DefaultInstance == null)
            {
                var credentialsPath = configuration["Firebase:CredentialsPath"] ?? "rahtk-ecommerce-app-firebase-adminsdk-73fmy-8084308673.json";
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Path.Combine("", credentialsPath)),
                });
            }


            services.AddHangfire(_ => _
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
                  .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                  {
                      CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                      SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                      QueuePollInterval = TimeSpan.Zero,
                      UseRecommendedIsolationLevel = true,
                      UsePageLocksOnDequeue = true,
                      DisableGlobalLocks = true
                  })
           );

            services.AddHangfireServer();
            services.AddHostedService<StartupBackgroundService>();
        }

        private static void AddSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RahtkContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Rahtk.Infrastructure")));
        }

        private static void SetUpIdentity(IServiceCollection services, IConfiguration configuration)
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration["Jwt:Key"] ?? ""
                    ))
                };
            });
        }
    }

}

