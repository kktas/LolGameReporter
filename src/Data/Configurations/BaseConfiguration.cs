using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            if (builder.Metadata.FindProperty("Id") != null)
            {
                builder.HasKey("Id");

                builder.Property("Id")
                    .ValueGeneratedOnAdd();
            }

            if (builder.Metadata.FindProperty("CreatedAt") != null)
            {
                builder.Property("CreatedAt")
                    .IsRequired();
            }

            if (builder.Metadata.FindProperty("CreatedBy") != null)
            {
                builder.Property("CreatedBy")
                    .IsRequired();
            }


            if (builder.Metadata.FindProperty("DeletedAt") != null)
            {
                builder.HasQueryFilter(x => EF.Property<DateTime?>(x, "DeletedAt") == null);
            }
        }
    }
}
