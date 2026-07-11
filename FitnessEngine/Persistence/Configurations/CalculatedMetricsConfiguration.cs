using FitnessEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessEngine.Persistence.Configurations
{
    public class CalculatedMetricsConfiguration : IEntityTypeConfiguration<CalculatedMetrics>
    {
        public void Configure(EntityTypeBuilder<CalculatedMetrics> builder)
        {
            builder.ToTable("CalculatedMetrics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.Property(x => x.Bmr).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(x => x.Tdee).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(x => x.CalorieTarget).HasColumnType("decimal(8,2)").IsRequired();
        }
    }
}
