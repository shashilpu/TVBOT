using Microsoft.EntityFrameworkCore;
using TVBot.Model.Entities;
namespace TVBot.SqlServer
{
    public partial class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }       
        DbSet<TradeOpportunity> Tradeopportunities { get; set; }   
        DbSet<TradeExecution> TradeExecutions { get; set; }
    }
}
