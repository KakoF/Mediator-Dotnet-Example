using Azure.Core;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Viabilidade.API.Extensions
{
    public static class ConfigureKeyVaultExtension
    {
        public static IConfigurationBuilder Configure(IConfigurationBuilder configurationBuilder, string uriKeyVault)
        {
            return configurationBuilder.AddAzureKeyVault(BuildSecretClient(uriKeyVault), BuildKeyVaultOptions());
        }

        private static SecretClient BuildSecretClient(string keyVaultUri)
        {
            var secretClientOptions = new SecretClientOptions();
            secretClientOptions.Retry.MaxRetries = 3;
            secretClientOptions.Retry.Mode = RetryMode.Fixed;
            secretClientOptions.Retry.Delay = TimeSpan.FromSeconds(1);
            secretClientOptions.Retry.NetworkTimeout = TimeSpan.FromSeconds(10);
            var uri = new Uri(keyVaultUri);

            return new SecretClient(uri, new DefaultAzureCredential(), secretClientOptions);
        }

        private static AzureKeyVaultConfigurationOptions BuildKeyVaultOptions()
        {
            return new AzureKeyVaultConfigurationOptions
            {
                ReloadInterval = TimeSpan.FromHours(12),
            };
        }
    }
}

