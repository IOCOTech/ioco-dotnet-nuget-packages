using Azure.Messaging.ServiceBus;
using Ioco.Infrastructure.Queue.Interface;

namespace Ioco.Infrastructure.Queue.Azure
{
    internal class AzureServiceBusProcessor : IMessageBusReciever
    {
        private readonly ServiceBusProcessor _serviceBusProcessor;

        internal AzureServiceBusProcessor(ServiceBusProcessor serviceBusProcessor)
        {
            _serviceBusProcessor = serviceBusProcessor;
        }

        public void SetHandlers(Func<ProcessMessageEventArgs, Task> messageHandler, Func<ProcessErrorEventArgs, Task> errorHandler)
        {
            _serviceBusProcessor.ProcessMessageAsync += messageHandler;
            _serviceBusProcessor.ProcessErrorAsync += errorHandler;
        }

        public async Task StartProcessingAsync()
        {
            await _serviceBusProcessor.StartProcessingAsync();
        }

        public async Task StopProcessorAsync()
        {
            await _serviceBusProcessor.StopProcessingAsync();
        }

        public async Task DisposeProcessorAsync()
        {
            await _serviceBusProcessor.DisposeAsync();
        }
    }
}
