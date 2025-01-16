using Npgsql;

namespace Postgres
{
    internal abstract class Program
    {
        private static void Main()
        {
            try
            {
                // Connect to the PostgreSQL database
                Console.WriteLine("Connecting to database");
                const string connString = "Server=postgres.server.com;Port=5432;Database=postgres;UserId=user;Password=password;";

                using var conn = new NpgsqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connection successful");

                // Perform a sample query to retrieve data from a table in the database
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT version();";

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}", reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
    }
}
