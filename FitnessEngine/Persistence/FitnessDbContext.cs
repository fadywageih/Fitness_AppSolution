using FitnessEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FitnessEngine.Persistence
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options) { }

        public DbSet<UserFitnessStats> FitnessStats => Set<UserFitnessStats>();
        public DbSet<CalculatedMetrics> Metrics => Set<CalculatedMetrics>();
        public DbSet<FitnessPlanConfig> PlanConfigs => Set<FitnessPlanConfig>();
        public DbSet<UserAssignedPlan> AssignedPlans => Set<UserAssignedPlan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
