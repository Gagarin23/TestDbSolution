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

            var dataBase = MongoClient.GetDatabase(dbName);
            var collection = dataBase.GetCollection<T>(tableName);

            await collection.InsertManyAsync(inputCollection);
        }
    }
}
