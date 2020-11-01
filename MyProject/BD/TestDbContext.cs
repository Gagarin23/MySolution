using Microsoft.EntityFrameworkCore;
using MyProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BD
{
    class TestDbContext : DbContext
    {
        public TestDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Shop> Shop { get; set; }
        public DbSet<Offer> Offer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=TestDataBase;Trusted_Connection=True;");
        }
    }
}
