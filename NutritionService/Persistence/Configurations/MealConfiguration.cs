using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{

    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.ToTable("Meals");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Ingredients).HasMaxLength(1000);
            builder.Property(x => x.Instructions).HasMaxLength(1000);
        }
    }
}
