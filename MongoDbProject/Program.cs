using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDbProject.BL.MongoDataBase;
using MongoDbProject.BL.XmlDeserializers;
using MongoDbProject.Model;

namespace MongoDbProject
{
    class Program
    {
        private static string url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
        private static string connectionString = @"mongodb://localhost:27017";
        private static string searchElement = @"shop";
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var xmlHandler = new XmlHandler(url);
            var shop = xmlHandler.GetElement<Shop>(searchElement);
            shop.SetIds();

            var dbHandler = new DataBaseHandler(connectionString);
            dbHandler.SaveToDbAsync(new[]{ shop }, "OZON", "Shops");

            Console.ReadLine();
        }
    }
}
