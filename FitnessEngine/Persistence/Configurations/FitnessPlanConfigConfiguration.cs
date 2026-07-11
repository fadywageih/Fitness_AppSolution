using FitnessEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessEngine.Persistence.Configurations
{

    public class FitnessPlanConfigConfiguration : IEntityTypeConfiguration<FitnessPlanConfig>
    {
        public void Configure(EntityTypeBuilder<FitnessPlanConfig> builder)
        {
            builder.ToTable("FitnessPlanConfigs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.PlanName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
        }
    }
}
