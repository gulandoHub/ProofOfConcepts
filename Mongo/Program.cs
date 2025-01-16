using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo;

internal abstract class Program
{
    private static void Main()
    {
        const string connectionString = "mongodb://user:password@mongo.gulando.com:27017";
        const string databaseName = "testdb";
        const string collectionName = "testcollection";
        
        var client = new MongoClient(connectionString);
        
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<BsonDocument>(collectionName);
        
        var document = new BsonDocument { { "name", "John Doe" } };
        collection.InsertOne(document);
        
        var result = collection.Find(document).FirstOrDefault();
        
        client.DropDatabase(databaseName);
        Console.WriteLine(result.ToJson());
    }
}
