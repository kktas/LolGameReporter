using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ChatConfiguration : BaseConfiguration<Chat>
    {
        public override void Configure(EntityTypeBuilder<Chat> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.TelegramChatId)
                .IsRequired();

            builder.ToTable("t_chat", "main");
        }
    }
}
