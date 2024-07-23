using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TVBot.Logger;
using TVBot.Repository.SqlServer;
using TVBot.Services.Factory;
using TVBot.Services.SqlServer;
using TVBot.SqlServer;



namespace TVBot
{
    public class Program
    {
        static async Task Main(string[] args)
        {          

            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An exception was caught and handled From Main.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService(option => option.ServiceName = "TVBotService")
                .ConfigureServices(async (context, services) =>
                {
                    services.AddDbContext<SqlServerDbContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DB_CONNECTION_STRING")), ServiceLifetime.Transient);
                    services.AddTransient(typeof(ISQLServer<>), typeof(SQLServer<>));
                    services.AddTransient(typeof(ISQLServerService<>), typeof(SQLServerService<>));
                    services.AddTransient<ISQLServerServiceFactory, SQLServerServiceFactory>();
                    services.AddHostedService<Worker>();
                    services.AddMemoryCache();
                    services.AddSingleton<LoggingConfiguration>();
                    LoggingConfiguration.ConfigureLogging(context.Configuration);
                    services.AddLogging(loggingbuilder =>
                    {
                        loggingbuilder.AddSerilog(dispose: true);
                    });

                });
        }

        static  void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error($"Unhandled exception: {e.ExceptionObject}");
        }
    }

}




