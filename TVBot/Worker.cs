using Azure.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TVBot.Model.Entities;
using TVBot.Services.Factory;
using TVBot.Services.SqlServer;
using TVBot.Utility;

namespace TVBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private const int DelayInMilliseconds = 10000;
        private readonly ISQLServerServiceFactory _sqlserviceFactory;
        public Worker(ILogger<Worker> logger, ISQLServerServiceFactory sqlserviceFactory)
        {
            _logger = logger;
            _sqlserviceFactory = sqlserviceFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Start.Begin(_sqlserviceFactory, _logger);
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(DelayInMilliseconds, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message.ToString());
                }


            }
        }
        public override Task StopAsync(CancellationToken stoppingToken)
        {
            //UtiityServices.SendReport(_sqlserviceFactory);
            //UtiityServices.SendTodayTradeExecutinReport(_sqlserviceFactory);
            //UtiityServices.SendTodayTradeCloseReport(_sqlserviceFactory);

            _logger.LogError("Worker stopped at: {time}", DateTimeOffset.Now);
            return base.StopAsync(stoppingToken);
        }
    }
}
