using Microsoft.Extensions.Hosting;

namespace TVBot
{
    public class Worker : IHostedService
    {
        public Task  StartAsync(CancellationToken cancellationToken)
        {
            Start.Begin();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}