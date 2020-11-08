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
        public static Offer offer = new Offer();
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var xmlHandler = new XmlHandler(url);
            var shop = xmlHandler.GetElement<Shop>(searchElement);
            shop.SetIds();
            //Console.WriteLine(b);
            //Console.WriteLine(a.Exception?.Message);
            //new DataBaseHandler();

            var dbHandler = new DataBaseHandler(connectionString);
            dbHandler.SaveToDbAsync(new[]{ shop }, "OZON", "Shops");

            Console.ReadLine();
        }

        static void Ser<T>(T obj)
        {
            var formatter = new XmlSerializer(typeof(T));
            using (var fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }

        //static Shop GetShop()
        //{
        //    return new Shop()
        //    {
        //        Currencies = new List<Currency>()
        //        {
        //            new Currency()
        //            {
        //                Name = "testCur",
        //                Rate = 1
        //            }
        //        },
        //        Offers = new List<Offer>()
        //        {
        //            new Offer()
        //            {
        //                CategoryId = new List<int>(){1,2},
        //                Location = new List<string>()
        //                {
        //                    "склад",
        //                    "магазин"
        //                }
                        
        //            }
        //        }
        //    };
        //}
    }
}
