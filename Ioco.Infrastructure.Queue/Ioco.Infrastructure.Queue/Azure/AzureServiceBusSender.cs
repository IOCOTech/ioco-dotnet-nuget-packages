using Azure.Messaging.ServiceBus;
using Ioco.Infrastructure.Queue.Interface;
using System.Text;
using System.Text.Json;

namespace Ioco.Infrastructure.Queue.Azure
{
    internal class AzureServiceBusSender : IMessageBusSender
    {
        private readonly ServiceBusSender _serviceBusSender;

        internal AzureServiceBusSender(ServiceBusSender serviceBusSender)
        {
            this._serviceBusSender = serviceBusSender;
        }

        public async Task<string> PublishMessageAsync(object message)
        {
            var jsonString = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonString));
            serviceBusMessage.MessageId = Guid.NewGuid().ToString();

            await _serviceBusSender.SendMessageAsync(serviceBusMessage);

            return serviceBusMessage.MessageId;
        }

        public async Task DisposeSenderAsync()
        {
            await _serviceBusSender.DisposeAsync();
        }
    }
}
