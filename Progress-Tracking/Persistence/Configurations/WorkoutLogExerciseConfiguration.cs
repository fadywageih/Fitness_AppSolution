using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Progress_Tracking.Domain.Entities;

namespace Progress_Tracking.Persistence.Configurations
{

    public class WorkoutLogExerciseConfiguration : IEntityTypeConfiguration<WorkoutLogExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutLogExercise> builder)
        {
            builder.ToTable("WorkoutLogExercises");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
