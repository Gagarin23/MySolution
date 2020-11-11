using System;
using System.Threading.Channels;
using ExtendedProject.Model;
using Microsoft.EntityFrameworkCore;

namespace ExtendedProject.BL.MSSQL
{
    sealed class TestDbContext : DbContext
    {
        public TestDbContext(bool isDel = false)
        {
            if(isDel)
                Del();
            Database.EnsureCreated();
        }
        void Del()
        {
            Database.EnsureDeleted();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=TestDataBase2;Trusted_Connection=True;");
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
