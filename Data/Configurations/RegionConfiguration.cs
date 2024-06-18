using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class RegionConfiguration : BaseConfiguration<Region>
    {
        public override void Configure(EntityTypeBuilder<Region> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name)
                .IsRequired();

            builder.ToTable("t_region", "main");
        }
    }
}
