using Microsoft.EntityFrameworkCore;
using SmartCoach.Domain.Entities;
using System.Reflection;

namespace SmartCoach.Persistence
{

    public class SmartCoachDbContext : DbContext
    {
        public SmartCoachDbContext(DbContextOptions<SmartCoachDbContext> options) : base(options) { }

        public DbSet<ChatSession> Sessions => Set<ChatSession>();
        public DbSet<ChatMessage> Messages => Set<ChatMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
