using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rahtk.Shared.Exceptions;
using Rahtk.Shared.Services;

namespace Rahtk.Shared
{
    public static class Extensions
    {
        public static void AddShared(this IServiceCollection services)
        {
            //services.AddHostedService<AppInitializer>();
            //services.AddScoped<ExceptionMiddleware>();
            services.RegisterLocalizationService();
        }
        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            //app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}

