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

        public static void RegisterInfrastructureDependancy(this IServiceCollection services, IConfiguration configuration) {
            AddSQL(services, configuration);
            services.AddScoped<IUserNotifier, UserNotifier>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        

        private static void AddSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RahtkContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Rahtk.Api")));
        }
    }
}

