using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using System.Reflection;

namespace ProfileService.Persistence
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) { }

        public DbSet<UserProfile> Profiles => Set<UserProfile>();
        public DbSet<UserPreference> Preferences => Set<UserPreference>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
