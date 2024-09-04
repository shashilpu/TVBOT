using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBot.Model.Entities;

namespace TVBot.SqlServer.Entities
{
    public class NewsPublishedTimesConfiguration : IEntityTypeConfiguration<NewsPublishedTime>
    {
        public void Configure(EntityTypeBuilder<NewsPublishedTime> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn(1, 1).HasColumnOrder(1);
            builder.Property(e => e.NSENIFTYPublishedTime).IsRequired().HasPrecision(19, 4).HasColumnOrder(2); ;
            builder.Property(e => e.NSEBankNIFTYPublishedTime).IsRequired().HasPrecision(19, 4).HasColumnOrder(3); ;

        }
    }
}
