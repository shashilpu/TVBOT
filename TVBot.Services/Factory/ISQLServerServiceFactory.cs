using TVBot.Services.SqlServer;

namespace TVBot.Services.Factory
{
    public interface ISQLServerServiceFactory
    {
        ISQLServerService<T> Create<T>() where T : class;
    }
}
