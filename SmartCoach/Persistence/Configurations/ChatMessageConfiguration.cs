using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCoach.Domain.Entities;

namespace SmartCoach.Persistence.Configurations
{

    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Sender).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(2000).IsRequired();
        }
    }
}
