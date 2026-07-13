using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Persistence.Configurations
{

    public class BillingLogConfiguration : IEntityTypeConfiguration<BillingLog>
    {
        public void Configure(EntityTypeBuilder<BillingLog> builder)
        {
            builder.ToTable("BillingLogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.UserId);
            builder.Property(x => x.PlanTier).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PaymentStatus).HasMaxLength(20).IsRequired();
        }
    }

}
