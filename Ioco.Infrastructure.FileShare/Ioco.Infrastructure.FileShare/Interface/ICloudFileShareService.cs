namespace Ioco.Infrastructure.FileShare.Interface
{
    public interface ICloudFileShareService
    {
        Task<byte[]> GetFileBytesAsync(string fileName, string directory);
        Task<string> UploadFileAsync(string fileName, byte[] fileContent, string directory);
    }
}
