using Azure.Messaging.ServiceBus;

namespace Ioco.Infrastructure.Queue.Interface
{
    public interface IMessageBusReciever
    {
        void SetHandlers(Func<ProcessMessageEventArgs, Task> messageHandler, Func<ProcessErrorEventArgs, Task> errorHandler);
        Task StartProcessingAsync();
        Task StopProcessorAsync();
        Task DisposeProcessorAsync();
    }
}
