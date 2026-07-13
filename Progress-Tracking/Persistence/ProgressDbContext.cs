using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Domain.Entities;
using System.Reflection;

namespace Progress_Tracking.Persistence
{

    public class ProgressDbContext : DbContext
    {
        public ProgressDbContext(DbContextOptions<ProgressDbContext> options) : base(options) { }

        public DbSet<WorkoutLog> WorkoutLogs => Set<WorkoutLog>();
        public DbSet<WorkoutLogExercise> WorkoutLogExercises => Set<WorkoutLogExercise>();
        public DbSet<WeightHistory> WeightHistory => Set<WeightHistory>();
        public DbSet<UserStatistics> UserStatistics => Set<UserStatistics>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
