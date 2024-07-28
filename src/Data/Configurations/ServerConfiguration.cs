using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ServerConfiguration : BaseConfiguration<Server>
    {
        public override void Configure(EntityTypeBuilder<Server> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name)
                .IsRequired();

            builder.HasOne(s => s.Region)
                .WithMany(r => r.Servers)
                .HasForeignKey(a => a.RegionId);
        }
    }
}
