﻿using System;
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
        private static string url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
        private static string searchElement = @"shop";

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var xmlHandler = new XmlHandler(url);
            var shop = xmlHandler.GetElement<Shop>(searchElement);
            MigrationToDB(shop);

            Console.ReadLine();
        }

        static void MigrationToDB(Shop shop)
        {
            var tempShop = new Shop(){Name = shop.Name};
            using (var db = new TestDbContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Shops.Add(tempShop);
                db.SaveChanges();
            }
            using (var db = new TestDbContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.OpenConnection();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON;");
                tempShop = db.Shops.Find(shop.Name);
                tempShop.Categories.AddRange(shop.Categories);
                db.Shops.Update(tempShop);
                db.SaveChanges();
            }
            //using (var db = new TestDbContext())
            //{
            //    db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //    db.Database.OpenConnection();
            //    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");

            //    var vendors = shop.Offers.Select(o => o.Vendor).Distinct().ToList();

            //    vendors.RemoveAll(v => v == null);
            //    vendors.ForEach(v => shop.Offers
            //        .Where(o => o.Vendor?.Name == v.Name).ToList()
            //        .ForEach(o => v.Offers.Add(o)));

            //    db.Vendors.AddRange(vendors);
            //    db.SaveChanges();
            //}
            using (var db = new TestDbContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Database.OpenConnection();
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");
                db.Offers.AddRange(shop.Offers);
                db.SaveChanges();
            }
            using (var db = new TestDbContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.Shops.Add(shop);
                db.SaveChanges();
            }
            using (var db = new TestDbContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }
    }
}
