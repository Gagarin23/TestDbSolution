using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EfProject.BL.MSSQL;
using EfProject.BL.XmlDeserializers;
using EfProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EfProject
{
    class Program
    {
        private static string url = @"http://static.ozone.ru/multimedia/yml/facet/mobile_catalog/1133677.xml";
        private static string searchElement = @"shop";

        static void Main(string[] args)
        {
            new TestDbContext(true);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var xmlHandler = new XmlHandler(url);
            var shop = xmlHandler.GetElement<Shop>(searchElement);
            MigrationToDB(shop);

            Console.ReadLine();
        }

        static void MigrationToDB(Shop shop)
        {
            SetCategoriesForOffers(shop);
            SetCurrenciesForOffers(shop);

            using (var db = new TestDbContext())
            {
                var foundedShop = db.Shops
                    .Include(s => s.Offers)
                    .SingleOrDefault(s => s.ShopId == shop.ShopId);

                if (foundedShop == null)
                {
                    db.Currencies.AddRange(shop.XmlCurrencies);
                    db.SaveChanges();

                    db.Database.OpenConnection();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON;");
                    db.Categories.AddRange(shop.Categories);
                    db.SaveChanges();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories OFF;");

                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");
                    db.Offers.AddRange(shop.Offers);
                    db.SaveChanges();
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers OFF;");

                    db.Shops.Add(shop);
                    db.SaveChanges();
                }
            }
        }

        static void SetCategoriesForOffers(Shop shop)
        {
            var categories = shop.Categories;

            foreach (var offer in shop.Offers)
            {
                offer.CategoryId.ForEach(oc => offer.Categories.Add(
                    categories.SingleOrDefault(sc => sc.Id == oc)));
            }
        }

        static void SetCurrenciesForOffers(Shop shop)
        {
            var categories = shop.Currencies;

            foreach (var offer in shop.Offers)
            {
                offer.Currency = shop.Currencies.SingleOrDefault(cur => cur.Name == offer.CurrencyId);
            }
        }
    }
}
