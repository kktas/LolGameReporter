using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ChampionConfiguration : BaseConfiguration<Champion>
    {
        public override void Configure(EntityTypeBuilder<Champion> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ChampionId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
