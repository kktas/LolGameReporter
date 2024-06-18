using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed
{
    internal static partial class Seeder
    {
        internal static ModelBuilder SeedServerData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>().HasData(
                new Server() { Id = 1, Name = "NA", RegionId = 1 },
                new Server() { Id = 2, Name = "EUW", RegionId = 2 },
                new Server() { Id = 3, Name = "EUNE", RegionId = 2 },
                new Server() { Id = 4, Name = "TR", RegionId = 2 },
                new Server() { Id = 5, Name = "JP", RegionId = 3 }
            );

            return modelBuilder;
        }
    }
}
