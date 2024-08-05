﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVBot.Model.Entities;

namespace TVBot.SqlServer.Entities
{
    public class TradeExecutionConfiguration : IEntityTypeConfiguration<TradeExecution>
    {
        public void Configure(EntityTypeBuilder<TradeExecution> builder)
        {
            builder.HasKey(x => x.TradeExecutionId);
            builder.Property(x => x.TradeExecutionId).UseIdentityColumn(1, 1).HasColumnOrder(1);
            builder.Property(x => x.Ticker).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.ExecutionDateTime).IsRequired().HasColumnOrder(12);
            builder.Property(x => x.ExecutionPrice).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.Quantity).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.TrargetPrice).IsRequired().HasColumnOrder(5);
            builder.Property(x => x.InTrade).IsRequired().HasColumnOrder(14);
            builder.Property(x => x.TradeType).IsRequired().HasColumnOrder(15);
            builder.Property(x => x.TargetPercentGain).IsRequired().HasColumnOrder(16);
            builder.Property(x => x.IsRepeatedTrade).IsRequired().HasColumnOrder(17);
            builder.Property(x => x.TradeOpportunityId).IsRequired().HasColumnOrder(18);
            builder.HasOne(x => x.TradeOpportunity).WithMany().HasForeignKey(x => x.TradeOpportunityId);
            builder.Property(x => x.ExecutionFee).HasColumnOrder(19);
            builder.Property(x => x.Notes).HasColumnOrder(20);
            builder.Property(x => x.IsTradeFromPastBullCross).HasColumnOrder(21);
            builder.Property(x => x.PastBullCrossInfo).HasColumnOrder(22);
            builder.Property(x => x.Status).HasColumnOrder(23);
            builder.Property(x => x.TradeCloseDateTime).HasColumnOrder(13);
            builder.Property(x => x.CurrentPrice).HasColumnOrder(6);
            builder.Property(x => x.InvestedAmount).HasColumnOrder(7);
            builder.Property(x => x.CurrentProfitLossOnTrade).HasColumnOrder(8);
            builder.Property(x => x.TradeClosePrice).HasColumnOrder(9);
            builder.Property(x => x.ProfitLoss).HasColumnOrder(10);
            builder.Property(x => x.PercentProfitLoss).HasColumnOrder(11);


        }
    }
}