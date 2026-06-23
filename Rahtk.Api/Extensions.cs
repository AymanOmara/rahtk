using Rahtk.Shared.Localization;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Rahtk.Api
{
    public static class Extensions
    {
        public static void SetUpApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetUpLocalization(services, configuration);
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

