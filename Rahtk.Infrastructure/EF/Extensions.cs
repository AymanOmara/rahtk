using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rahtk.Contracts.Common;
using Rahtk.Infrastructure.Common;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Infrastructure.EF.Services;

namespace Rahtk.Infrastructure.EF
{
    public static class Extensions
    {

        public static void RegisterInfrastructureDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            AddSQL(services, configuration);
            services.AddScoped<IUserNotifier, UserNotifier>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IFileService, FileService>();

            services.AddSingleton<INotificationSender, NotificationSender>();
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine("", "rahtk-ecommerce-app-firebase-adminsdk-73fmy-8084308673.json")),
            });
            services.AddHangfire(_ => _
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
                  .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                  {
                      CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                      SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                      QueuePollInterval = TimeSpan.Zero,
                      UseRecommendedIsolationLevel = true,
                      UsePageLocksOnDequeue = true,
                      DisableGlobalLocks = true
                  })
           );

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        private static void AddSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RahtkContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Rahtk.Api")));
        }

    }
}

