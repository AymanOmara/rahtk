using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF
{
    public static class Extensions
    {
        public static void AddSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RahtkContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Rahtk.Infrastructure")));
        }
    }
}

