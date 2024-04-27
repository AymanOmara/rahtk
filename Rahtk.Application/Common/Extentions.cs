using Microsoft.Extensions.DependencyInjection;
using Rahtk.Application.Features;
using Rahtk.Application.Features.User;

namespace Rahtk.Application.Common
{
	public static class Extentions
	{
		public static void RegisterApplicationDependancies(this IServiceCollection services) {
			services.AddScoped<IUserService, UserService>();
		}
	}
}

