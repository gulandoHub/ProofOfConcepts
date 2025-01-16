using RestSharp;

namespace Grafana
{
    internal abstract class Program
    {
        private static void Main()
        {
            const string baseUrl = "http://grafana.gulando.com";
            const string username = "admin";
            const string password = "password here";

            var authHeader = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            
            var client = new RestClient(baseUrl);
            client.AddDefaultHeader("Authorization", $"Basic {authHeader}");

            const string dashboardJson = """
                                         
                                                     {
                                                         "dashboard": {
                                                             "id": null,
                                                             "title": "My C# Dashboard",
                                                             "panels": [],
                                                             "timezone": "browser",
                                                             "schemaVersion": 21,
                                                             "version": 0
                                                         },
                                                         "folderId": 0,
                                                         "overwrite": false
                                                     }
                                         """;

            var request = new RestRequest("Post");
            request.AddJsonBody(dashboardJson);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Dashboard created successfully!");
            }
            else
            {
                Console.WriteLine("Error creating dashboard.");
                Console.WriteLine(response.Content);
            }
        }
    }
}
