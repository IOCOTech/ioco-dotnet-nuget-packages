using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace Ioco.Infrastructure.Vault
{
    public static class ModuleInitializer
    {
        public static IConfigurationBuilder AddVaultSupport(this IConfigurationBuilder configHost)
    {
        var builtConfig = configHost.Build();

        var vault = builtConfig["KeyVault"];

        if (!string.IsNullOrWhiteSpace(vault))
        {
            var secretClient = new SecretClient(new Uri(vault), new DefaultAzureCredential());
            configHost.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
        }

        return configHost;
    }
    }
}
