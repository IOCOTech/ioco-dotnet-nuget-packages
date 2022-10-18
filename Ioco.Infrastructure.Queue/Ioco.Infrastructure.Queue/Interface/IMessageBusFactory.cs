namespace Ioco.Infrastructure.Queue.Interface
{
    public interface IMessageBusFactory
    {
        IMessageBusSender GetSenderClient(string queueName);
        IMessageBusReciever GetReceiverClient(string queueName);
    }
}
