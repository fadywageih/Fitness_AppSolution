using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Persistence.Configurations
{

    public class InAppNotificationConfiguration : IEntityTypeConfiguration<InAppNotification>
    {
        public void Configure(EntityTypeBuilder<InAppNotification> builder)
        {
            builder.ToTable("InAppNotifications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => new { x.UserId, x.IsRead });
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(500).IsRequired();
        }
    }
}
