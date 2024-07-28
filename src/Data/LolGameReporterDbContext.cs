using Core.Models;
using Data.Configurations;
using Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class LolGameReporterDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Champion> Champions { get; set; }
        public LolGameReporterDbContext(DbContextOptions<LolGameReporterDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChampionConfiguration());
            modelBuilder.ApplyConfiguration(new ServerConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());

            modelBuilder
                .SeedRegionData()
                .SeedServerData();
        }
    }
}
