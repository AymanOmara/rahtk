using Hangfire;
using Microsoft.Extensions.Hosting;

namespace Rahtk.Infrastructure.EF.Services
{
	public class StartupBackgroundService(IBackgroundJobClient backgroundJobClient) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Enqueue a job to run on server startup
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hangfire job started on server startup"));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Nothing to do here
            return Task.CompletedTask;
        }
    }

}

