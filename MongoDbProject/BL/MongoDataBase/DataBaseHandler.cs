using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MongoDbProject.BL.MongoDataBase
{
    class DataBaseHandler
    {
        private const string Table = "Shop";
        private const string DbName = "TestBase";
        private MongoClient MongoClient;
        public string ConnectionString { get; set; }

        public DataBaseHandler(string connectionString)
        {
            if(string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException();

            ConnectionString = connectionString;
            MongoClient = new MongoClient(ConnectionString);
        }

        public async void SaveToDbAsync<T>(IEnumerable<T> inputCollection, string dbName, string tableName)
        {
            if(inputCollection == null || !inputCollection.Any() || string.IsNullOrEmpty(dbName))
                throw new ArgumentNullException();

            var b = BsonClassMap.RegisterClassMap<T>();
            var dataBase = MongoClient.GetDatabase(dbName);
            var collection = dataBase.GetCollection<T>(tableName);

            await collection.InsertManyAsync(inputCollection);
        }

        async void NewFunction(IMongoDatabase mongoDatabase)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>(Table);
            var filter = new BsonDocument();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var ppl = cursor.Current;
                    foreach (var doc in ppl)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
        }

        private static async Task GetCollectionsNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db["name"]);
 
                    IMongoDatabase database = client.GetDatabase(db["name"].ToString());
 
                    using (var collCursor = await database.ListCollectionsAsync())
                    {
                        var colls = await collCursor.ToListAsync();
                        foreach (var col in colls)
                        {
                            Console.WriteLine(col["name"]);
                            var items = col.ToList();

                            foreach (var item in items)
                            {
                                Console.WriteLine(item.Name + ": " + item.Value);
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
