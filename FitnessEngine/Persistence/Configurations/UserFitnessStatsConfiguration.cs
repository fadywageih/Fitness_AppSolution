using FitnessEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessEngine.Persistence.Configurations
{

    public class UserFitnessStatsConfiguration : IEntityTypeConfiguration<UserFitnessStats>
    {
        public void Configure(EntityTypeBuilder<UserFitnessStats> builder)
        {
            builder.ToTable("UserFitnessStats");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.UserId);
            builder.Property(x => x.Weight).HasColumnType("decimal(5,1)").IsRequired();
            builder.Property(x => x.Height).HasColumnType("decimal(5,1)").IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(10).IsRequired();
        }
    }
}
