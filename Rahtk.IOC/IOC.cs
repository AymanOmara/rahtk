using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rahtk.Application.Common;
using Rahtk.Infrastructure.EF;
using Rahtk.Shared;

namespace Rahtk.IOC;

public static class IOC
{
    public static void RegisterIOCServices(this IServiceCollection service, IConfiguration configuration) {
        service.RegisterLocalizationService();
        service.RegisterInfrastructureDependancy(configuration);
        service.RegisterApplicationDependancies();
        service.AddShared();
    }
}

