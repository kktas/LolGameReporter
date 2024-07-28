using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed
{
    internal static partial class Seeder
    {
        internal static ModelBuilder SeedRegionData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>().HasData(
                new Region() { Id = 1, Name = "Americas" },
                new Region() { Id = 2, Name = "Europe" },
                new Region() { Id = 3, Name = "Asia" }
            );

            return modelBuilder;
        }
    }
}
