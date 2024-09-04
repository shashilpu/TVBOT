using Microsoft.EntityFrameworkCore;
using System.Xml;
using TVBot.Model.Entities;
using TVBot.SqlServer.Entities;
namespace TVBot.SqlServer
{
    public partial class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           modelBuilder.ApplyConfiguration(new NewsPublishedTimesConfiguration());
            modelBuilder.ApplyConfiguration(new TradeOpportunityConfiguration());
            modelBuilder.ApplyConfiguration(new TradeExecutionConfiguration());           
           
        }
        DbSet<NewsPublishedTime> NewsPublishedTimes { get; set; }
        DbSet<TradeOpportunity> Tradeopportunities { get; set; }
        DbSet<TradeExecution> TradeExecutions { get; set; }
    }
}
