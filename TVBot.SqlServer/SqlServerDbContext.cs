using Microsoft.EntityFrameworkCore;
using System.Xml;
using TVBot.Model.Entities;
namespace TVBot.SqlServer
{
    public partial class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TickerInfo>()
                .Property(e => e.TickerInfoId)
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<TradeOpportunityOneMin>()
                .Property(e => e.TradeOpportunityOneMinId)
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<TradeExecutionOneMin>()
                .Property(e => e.TradeExecutionOneMinId)
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<TradeOpportunity>()
                .Property(e => e.Id)
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<TradeExecution>()
                .Property(e => e.TradeExecutionId)
                .UseIdentityColumn(1, 1);
        }
        DbSet<TickerInfo> TickerInfos { get; set; }
        DbSet<TradeOpportunityOneMin> TradeOpportunityOneMins { get; set; }
        DbSet<TradeExecutionOneMin> TradeExecutionOneMins { get; set; }
        DbSet<TradeOpportunity> Tradeopportunities { get; set; }
        DbSet<TradeExecution> TradeExecutions { get; set; }
    }
}
