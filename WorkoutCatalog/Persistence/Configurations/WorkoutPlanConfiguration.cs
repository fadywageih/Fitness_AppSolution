using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Domain.Entities;

namespace WorkoutCatalog.Persistence.Configurations
{

    public class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.ToTable("WorkoutPlans");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
        }
    }

}
