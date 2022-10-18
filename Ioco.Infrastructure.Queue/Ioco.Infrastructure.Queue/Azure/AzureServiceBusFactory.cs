using Azure.Messaging.ServiceBus;
using Ioco.Infrastructure.Queue.Interface;

namespace Ioco.Infrastructure.Queue.Azure
{
    public class AzureServiceBusFactory : IMessageBusFactory
    {
        private readonly ServiceBusClient _serviceBusClient;

        public AzureServiceBusFactory(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public IMessageBusSender GetSenderClient(string queueName)
        {
            return new AzureServiceBusSender(_serviceBusClient.CreateSender(queueName));
        }

        public IMessageBusReciever GetReceiverClient(string queueName)
        {
            return new AzureServiceBusProcessor(_serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions()));
        }
    }
}
