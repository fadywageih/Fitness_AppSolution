using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WorkoutCatalog.Domain.Entities;

namespace WorkoutCatalog.Persistence
{
    public class WorkoutCatalogDbContext : DbContext
    {
        public WorkoutCatalogDbContext(DbContextOptions<WorkoutCatalogDbContext> options) : base(options) { }

        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<Workout> Workouts => Set<Workout>();
        public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
        public DbSet<WorkoutPlan> Plans => Set<WorkoutPlan>();
        public DbSet<WorkoutSession> Sessions => Set<WorkoutSession>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
