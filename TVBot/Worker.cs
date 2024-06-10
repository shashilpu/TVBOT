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
        private readonly ITradeOpportunityService _tradeOpportunityService;
        public Worker(ILogger<Worker> logger, ITradeOpportunityService tradeOpportunityService)
        {
            _logger = logger;
            _tradeOpportunityService = tradeOpportunityService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {                
                await _tradeOpportunityService.AddTradeOpportunity(new TradeOpportunity
                {                    
                    CrossOverDateTime = DateTime.Now,                    
                    Ticker = "AAPL",
                    PercentChange = "0.51223334444555556",
                    Price = "100",
                    AlgoName = "Simple Moving Average",
                    Volume = 1000
                });
               _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(DelayInMilliseconds, stoppingToken);
            }
        }
    }
}