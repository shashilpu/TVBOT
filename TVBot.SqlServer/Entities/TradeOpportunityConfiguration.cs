using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVBot.Model.Entities;

namespace TVBot.SqlServer.Entities
{
    public class TradeOpportunityConfiguration : IEntityTypeConfiguration<TradeOpportunity>
    {
        public void Configure(EntityTypeBuilder<TradeOpportunity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn(1, 1).HasColumnOrder(1);
            builder.Property(e => e.CrossOverDateTime).IsRequired().ValueGeneratedOnAdd().HasColumnOrder(9);
            builder.Property(e => e.Ticker).IsRequired().HasColumnOrder(2);
            builder.Property(e => e.PercentChange).IsRequired().HasPrecision(15, 4).HasColumnOrder(4);
            builder.Property(e => e.Price).IsRequired().HasPrecision(15,4).HasColumnOrder(3);
            builder.Property(e => e.AnalystRating).IsRequired().HasColumnOrder(5);
            builder.Property(e => e.BetaOneYear).IsRequired().HasPrecision(15, 4).HasColumnOrder(8);
            builder.Property(e => e.PercentVolalityOneWeek).IsRequired().HasPrecision(15, 4).HasColumnOrder(11);
            builder.Property(e => e.AlgoName).IsRequired().HasColumnOrder(6);
            builder.Property(e => e.Volume).IsRequired().HasPrecision(15, 4).HasColumnOrder(7);
            builder.Property(e => e.CrossOverType).IsRequired().HasColumnOrder(10);           

                
        }
    }
}