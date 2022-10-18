namespace Ioco.Infrastructure.Queue.Interface
{
    public interface IMessageBusSender
    {
        /// <summary>
        /// Places your message on the queue and returns a messageId
        /// </summary>
        /// <param name="message"></param>
        /// <returns>MessageId</returns>
        Task<string> PublishMessageAsync(object message);
        Task DisposeSenderAsync();
    }
}
