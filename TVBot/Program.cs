
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



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
        .ConfigureServices((hostContext, services) =>
        {
            //services.AddDbContext<DocumentContext>(options =>
            //   options.UseSqlite(hostContext.Configuration.GetConnectionString("DB_CONNECTION_STRING")));
            //services.AddTransient<IDocumentRepository, DocumentRepository>();
            //services.AddTransient<IDocumentService, DocumentService>();
            services.AddHostedService<Worker>();

        });


    }


}




