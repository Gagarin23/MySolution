using Microsoft.EntityFrameworkCore;
using MyProject.Model;

namespace MyProject.BD
{
    sealed class TestDbContext : DbContext
    {
        public TestDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<AvailabilityInShop> Availability { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=TestDataBase;Trusted_Connection=True;");
        }
    }
}
