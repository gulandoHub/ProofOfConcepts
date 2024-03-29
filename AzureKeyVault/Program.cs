using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

const string kvUri = "https://kv-poc-vault-neu.vault.azure.net/";

var client = new SecretClient(new Uri(kvUri), new ClientSecretCredential(
    "aecc83fe-fa66-4c0a-99c2-dd439c2b1c4d", 
    "847720d7-088a-45da-8626-fff4c30e8fba", 
    "plz8Q~lrpnEDTcFbDJizPINXaR2KAOJ-auO7kcuz"));

try
{
    var secret1 = client.GetSecret("my-secret");
    var secret2 = client.GetSecret("db-connection");
    
    var secretValue1 = secret1.Value;
    var secretValue2 = secret2.Value;


    Console.WriteLine(secretValue1.Value);
    Console.WriteLine(secretValue2.Value);

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}