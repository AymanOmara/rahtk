using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rahtk.Infrastructure.EF;
using Rahtk.Shared;

namespace Rahtk.IOC;

public static class IOC
{
    public static void RegisterServices(this IServiceCollection service, IConfiguration configuration) {
        service.RegisterLocalizationService();
        service.AddSQL(configuration);
        service.AddShared();
    }
}

