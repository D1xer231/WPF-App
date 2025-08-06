using MyStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Myapp8
{
    internal class AppDbContext : DbContext
    {

        public DbSet<Items> Users { get; set; }
        public object Items { get; internal set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            options.UseMySql(
                "server=localhost;port=3306;username=root;password=root;database=mainproject",
                serverVersion
            );
        }

        public DbSet<Items> Item { get; set; }

    }
}
