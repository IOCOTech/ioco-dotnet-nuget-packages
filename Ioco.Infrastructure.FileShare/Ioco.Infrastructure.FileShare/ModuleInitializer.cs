using Azure.Storage.Files.Shares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ioco.Infrastructure.FileShare.Azure;
using Ioco.Infrastructure.FileShare.Enums;
using Ioco.Infrastructure.FileShare.Interface;

namespace Ioco.Infrastructure.FileShare
{
    public static class ModuleInitializer
    {
        public static void AddCloudFileShare(this IServiceCollection services, IConfiguration configuration, CloudProviderEnum cloudProvider = CloudProviderEnum.Azure)
        {
            var fileShareSettings = configuration.GetSection(nameof(FileShareSettings)).Get<FileShareSettings>();
            services.Configure<FileShareSettings>(configuration.GetSection(nameof(FileShareSettings)));

            switch (cloudProvider)
            {
                case CloudProviderEnum.Azure:
                    services.AddSingleton(new ShareClient(fileShareSettings.ConnectionString, fileShareSettings.ShareName));
                    services.AddSingleton<ICloudFileShareService, AzureFileShareClient>();
                    break;
            }
        }
    }
}
