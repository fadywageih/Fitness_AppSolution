using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Domain.Entities;

namespace WorkoutCatalog.Persistence.Configurations
{

    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercises");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.TargetMuscles).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Equipment).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.VideoUrl).HasMaxLength(500);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
        }
    }
}
