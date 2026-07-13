using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Progress_Tracking.Domain.Entities;

namespace Progress_Tracking.Persistence.Configurations
{

    public class WeightHistoryConfiguration : IEntityTypeConfiguration<WeightHistory>
    {
        public void Configure(EntityTypeBuilder<WeightHistory> builder)
        {
            builder.ToTable("WeightHistory");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.UserId);
            builder.Property(x => x.Weight).HasColumnType("decimal(5,1)").IsRequired();
        }
    }
}
