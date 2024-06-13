using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TVBot.Model.Entities;
using TVBot.Services.SqlServer;

namespace TVBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private const int DelayInMilliseconds = 10000;
        private readonly ITradeOpportunityService<TradeOpportunity> _tradeOpportunityService;
        public Worker(ILogger<Worker> logger, ITradeOpportunityService<TradeOpportunity> tradeOpportunityService)
        {
            _logger = logger;
            _tradeOpportunityService = tradeOpportunityService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                Start.Begin(_tradeOpportunityService);
               
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(DelayInMilliseconds, stoppingToken);
            }
        }
    }
}