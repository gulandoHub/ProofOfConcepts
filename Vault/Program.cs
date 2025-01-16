using VaultSharp;
using VaultSharp.Core;
using VaultSharp.V1.AuthMethods.Token;

namespace Vault;

internal abstract class Program
{
    private static async Task Main()
    {
        // Vault configuration
        const string vaultUrl = "https://vault.server.com";
        const string token = "password";

        // Initialize Vault client
        var vaultClientSettings = new VaultClientSettings(vaultUrl, new TokenAuthMethodInfo(token));
        var vaultClient = new VaultClient(vaultClientSettings);

        // Path of the secret to retrieve
        const string secretPath = "API";

        try
        { 
            // Retrieve the secret from Vault
            var readSecretResponse = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretPath, mountPoint:"secret");

            if (readSecretResponse is { Data.Data: not null })
            {
                var secretData = readSecretResponse.Data.Data;

                Console.WriteLine("Retrieved Secret Data:");
                foreach (var kvp in secretData)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            else
            {
                Console.WriteLine("Secret not found.");
            }
        }
        catch (VaultApiException ex)
        {
            Console.WriteLine($"Error retrieving secret: {ex.Message}");
        }
    }
}
