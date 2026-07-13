using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entities;
using System.Reflection;

namespace SubscriptionService.Persistence
{

    public class SubscriptionDbContext : DbContext
    {
        public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options) : base(options) { }

        public DbSet<UserSubscription> Subscriptions => Set<UserSubscription>();
        public DbSet<BillingLog> BillingLogs => Set<BillingLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
