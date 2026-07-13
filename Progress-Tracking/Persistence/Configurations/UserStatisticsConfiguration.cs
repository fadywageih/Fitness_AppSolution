using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Progress_Tracking.Domain.Entities;

namespace Progress_Tracking.Persistence.Configurations
{

    public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
    {
        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder.ToTable("UserStatistics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}
