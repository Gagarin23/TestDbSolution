using System;
using System.Threading.Channels;
using EfProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EfProject.BL.MSSQL
{
    sealed class TestDbContext : DbContext
    {
        public TestDbContext(bool flag = false)
        {
            if(flag)
                Del();
            Database.EnsureCreated();
        }

        void Del()
        {
            Database.EnsureDeleted();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<A> DbSetA { get; set; }
        public DbSet<B> DbSetB { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=MSDataBase;Trusted_Connection=True;");
            //optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
