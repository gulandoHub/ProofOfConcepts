using StackExchange.Redis;

namespace Redis
{
    internal abstract class Program
    {
        private static void Main()
        {
            // Connect to Redis with a password
            var configOptions = new ConfigurationOptions
            {
                EndPoints =
                {
                    "redis.server.com:6379"
                },
                Password = "user"
            };
            
            var redis = ConnectionMultiplexer.Connect(configOptions);

            // Get a database instance
            var db = redis.GetDatabase();

            // Store an object using serialization
            var user = new User
            {
                Id = 1, 
                Name = "Alice",
                Age = 30
            };
            
            var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            db.StringSet("User:1", serializedUser);

            // Retrieve the object using deserialization
            var serializedUser2 = db.StringGet("User:1");
            var user2 = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(serializedUser2);

            // Print the object's properties
            if (user2 != null)
            {
                Console.WriteLine($"Id: {user2.Id}, Name: {user2.Name}, Age: {user2.Age}");
            }

            // Deletes all keys from the current database
            db.Execute("FLUSHDB");
        }
    }
}
