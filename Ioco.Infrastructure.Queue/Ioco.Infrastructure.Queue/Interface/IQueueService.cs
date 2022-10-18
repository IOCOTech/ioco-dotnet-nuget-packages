namespace Ioco.Infrastructure.Queue.Interface
{
    public interface IQueueService
    {
        Task<byte[]> SendMessage<T>(T t);
        Task<string> UploadFileAsync(string fileName, byte[] fileContent, string directory);
    }
}
