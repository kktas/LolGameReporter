using Core.Models;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class LolGameReporterDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public LolGameReporterDbContext(DbContextOptions<LolGameReporterDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
        }
    }
}
