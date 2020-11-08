using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EfProject.BL.MSSQL;
using EfProject.BL.XmlDeserializers;
using EfProject.Model;

namespace MongoDbProject
{
    class Program
    {
        private static string url = @"http://static.ozone.ru/multimedia/yml/facet/div_soft.xml";
        private static string searchElement = @"shop";
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            new TestDbContext();
            var xmlHandler = new XmlHandler(url);
            var shop = xmlHandler.GetElement<Shop>(searchElement);
            //Console.WriteLine(b);
            //Console.WriteLine(a.Exception?.Message);
            //new DataBaseHandler();

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
        //                Locations = new List<string>()
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
