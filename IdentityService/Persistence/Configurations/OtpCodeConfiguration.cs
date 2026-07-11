using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Persistence.Configurations
{
    public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
    {
        public void Configure(EntityTypeBuilder<OtpCode> builder)
        {
            builder.ToTable("OtpCodes");

            builder.HasKey(oc => oc.Id);
            builder.Property(oc => oc.Id).ValueGeneratedNever();
            builder.Property(oc => oc.CodeHash).HasMaxLength(500).IsRequired();
            builder.Property(oc => oc.Purpose).HasMaxLength(50).IsRequired();
            builder.Property(oc => oc.ExpiresAt).IsRequired();
            builder.Property(oc => oc.IsUsed).IsRequired();
            builder.Property(oc => oc.CreatedAt).IsRequired();

            builder.HasOne(oc => oc.User)
                .WithMany()
                .HasForeignKey(oc => oc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(oc => new { oc.UserId, oc.Purpose, oc.IsUsed });
        }
    }
}
