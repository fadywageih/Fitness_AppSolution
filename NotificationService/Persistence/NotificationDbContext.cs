using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using System.Reflection;

namespace NotificationService.Persistence
{

    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
        public DbSet<InAppNotification> Notifications => Set<InAppNotification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
