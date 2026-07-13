using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Progress_Tracking.Domain.Entities;

namespace Progress_Tracking.Persistence.Configurations
{

    public class WorkoutLogConfiguration : IEntityTypeConfiguration<WorkoutLog>
    {
        public void Configure(EntityTypeBuilder<WorkoutLog> builder)
        {
            builder.ToTable("WorkoutLogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.SessionId).IsUnique();
            builder.HasMany(x => x.Exercises).WithOne(x => x.WorkoutLog).HasForeignKey(x => x.WorkoutLogId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
