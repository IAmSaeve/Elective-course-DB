using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoClientProgram
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Air-Quality");
            var collection = database.GetCollection<BsonDocument>("Measurement");
            await collection.Find(new BsonDocument()).Limit(10).ForEachAsync(m => Console.WriteLine(m));
            
            Console.ReadKey();
        }
    }
}
