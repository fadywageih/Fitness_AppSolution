using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{

    public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
    {
        public void Configure(EntityTypeBuilder<MealPlan> builder)
        {
            builder.ToTable("MealPlans");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(x => x.Items).WithOne(x => x.MealPlan).HasForeignKey(x => x.MealPlanId).OnDelete(DeleteBehavior.Cascade);
        }
    }

}
