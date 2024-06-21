using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Data.Configurations
{
    public class AccountConfiguration : BaseConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.GameName)
                .IsRequired();

            builder.Property(a => a.TagLine)
                .IsRequired();

            builder.Property(a => a.Puuid)
                .IsRequired();

            builder.HasMany(s => s.Chats)
            .WithMany(c => c.Accounts)
            .UsingEntity<Dictionary<string, object>>(
                j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId"),
                j => j.HasOne<Account>().WithMany().HasForeignKey("AccountId"));

        }
    }
}
