using FitnessEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessEngine.Persistence.Configurations
{

    public class UserAssignedPlanConfiguration : IEntityTypeConfiguration<UserAssignedPlan>
    {
        public void Configure(EntityTypeBuilder<UserAssignedPlan> builder)
        {
            builder.ToTable("UserAssignedPlans");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => new { x.UserId, x.IsActive });
            builder.HasOne(x => x.PlanConfig).WithMany().HasForeignKey(x => x.PlanConfigId);
        }
    }
}
