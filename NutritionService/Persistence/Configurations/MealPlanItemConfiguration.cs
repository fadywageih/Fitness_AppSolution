using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{

    public class MealPlanItemConfiguration : IEntityTypeConfiguration<MealPlanItem>
    {
        public void Configure(EntityTypeBuilder<MealPlanItem> builder)
        {
            builder.ToTable("MealPlanItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Meal).WithMany().HasForeignKey(x => x.MealId);
            builder.Property(x => x.DayOfWeek).HasMaxLength(20);
            builder.Property(x => x.MealTime).HasMaxLength(20);
        }
    }
}
