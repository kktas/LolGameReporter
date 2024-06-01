using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.HasOne(a => a.Chat)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.ChatId);

            builder.ToTable("t_account", "main");
        }
    }
}
