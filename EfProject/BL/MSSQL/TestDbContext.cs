using EfProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EfProject.BL.MSSQL
{
    sealed class TestDbContext : DbContext
    {
        public TestDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<OfferGroup> OfferGroups { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=MSDataBase;Trusted_Connection=True;");
        }
    }
}
