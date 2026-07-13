using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Entities;
using System.Reflection;

namespace NutritionService.Persistence
{

    public class NutritionDbContext : DbContext
    {
        public NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options) { }

        public DbSet<Meal> Meals => Set<Meal>();
        public DbSet<MealPlan> MealPlans => Set<MealPlan>();
        public DbSet<MealPlanItem> MealPlanItems => Set<MealPlanItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
