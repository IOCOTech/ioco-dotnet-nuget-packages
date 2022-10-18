using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ioco.Infrastructure.Queue.Azure;
using Ioco.Infrastructure.Queue.Interface;

namespace Ioco.Infrastructure.Queue
{
    public static class ModuleInitializer
    {
        public static void AddAzureServiceBusSupport(this IServiceCollection services, IConfiguration configuration)
        {
            var queueSettings = configuration.GetSection(nameof(QueueSettings)).Get<QueueSettings>();
            services.Configure<QueueSettings>(configuration.GetSection(nameof(QueueSettings)));

            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(queueSettings.ConnectionString);
            });
            services.AddSingleton<IMessageBusFactory, AzureServiceBusFactory>();
        }
    }
}