using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TVBot.Model.Entities;
using TVBot.Repository.SqlServer;
using TVBot.Services.SqlServer;
using TVBot.SqlServer;



namespace TVBot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();           

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
        .UseWindowsService(option=>option.ServiceName="TVBotService")
        .ConfigureServices((context, services) =>
        {
            services.AddDbContext<SqlServerDbContext>(options =>
               options.UseSqlServer(context.Configuration.GetConnectionString("DB_CONNECTION_STRING")),ServiceLifetime.Singleton);
            services.AddTransient(typeof(ISQLServer<TradeOpportunity>), typeof(SQLServer<TradeOpportunity>));
            services.AddTransient<ITradeOpportunityService,TradeOpportunityService>();
            services.AddHostedService<Worker>();
            services.AddMemoryCache();

        });


    }


}




