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

           // modelBuilder.ApplyConfiguration(new TickerInfoConfiguration());
            modelBuilder.ApplyConfiguration(new TradeOpportunityConfiguration());
            modelBuilder.ApplyConfiguration(new TradeExecutionConfiguration());

            //modelBuilder.Entity<TickerInfo>()
            //    .Property(e => e.TickerInfoId)
            //    .UseIdentityColumn(1, 1);            

            //modelBuilder.Entity<TradeOpportunity>()
            //    .Property(e => e.Id)
            //    .UseIdentityColumn(1, 1);

            //modelBuilder.Entity<TradeExecution>()
            //    .Property(e => e.TradeExecutionId)
            //    .UseIdentityColumn(1, 1);
        }
       // DbSet<TickerInfo> TickerInfos { get; set; }      
        DbSet<TradeOpportunity> Tradeopportunities { get; set; }
        DbSet<TradeExecution> TradeExecutions { get; set; }
    }
}
