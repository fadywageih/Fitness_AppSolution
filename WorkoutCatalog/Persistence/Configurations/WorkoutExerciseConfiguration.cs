using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Domain.Entities;

namespace WorkoutCatalog.Persistence.Configurations
{

    public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder.ToTable("WorkoutExercises");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Exercise).WithMany().HasForeignKey(x => x.ExerciseId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.OrderIndex);
        }
    }

}
