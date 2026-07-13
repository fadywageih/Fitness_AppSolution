using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Domain.Entities;

namespace WorkoutCatalog.Persistence.Configurations
{


    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("Workouts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.HasOne(x => x.Plan)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(x => x.WorkoutExercises)
                .WithOne(x => x.Workout)
                .HasForeignKey(x => x.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.Category);
            builder.HasIndex(x => x.OrderIndex);
        }
    }
}
