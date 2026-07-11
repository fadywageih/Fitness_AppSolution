using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Persistence.Configurations
{
    public class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
    {
        public void Configure(EntityTypeBuilder<LoginAttempt> builder)
        {
            builder.ToTable("LoginAttempts");

            builder.HasKey(la => la.Id);
            builder.Property(la => la.Id).ValueGeneratedNever();
            builder.Property(la => la.AttemptedAt).IsRequired();
            builder.Property(la => la.IsSuccess).IsRequired();
            builder.Property(la => la.FailureReason).HasMaxLength(200).IsRequired(false);
            builder.Property(la => la.IpAddress).HasMaxLength(50).IsRequired(false);

            builder.HasOne(la => la.User)
                .WithMany()
                .HasForeignKey(la => la.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(la => new { la.UserId, la.AttemptedAt });
        }
    }
}
