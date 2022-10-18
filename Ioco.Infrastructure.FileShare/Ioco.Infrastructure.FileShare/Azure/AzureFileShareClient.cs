using Azure;
using Azure.Storage.Files.Shares;
using Microsoft.Extensions.Options;
using Ioco.Infrastructure.FileShare.Interface;

namespace Ioco.Infrastructure.FileShare.Azure
{
    public class AzureFileShareClient : ICloudFileShareService
    {
        private readonly ShareClient _client;
        private readonly FileShareSettings _fileShareSettings;
        public AzureFileShareClient(ShareClient client, IOptions<FileShareSettings> fileShareSettings)
        {
            _client = client;
            _fileShareSettings = fileShareSettings.Value;
        }

        public async Task<byte[]> GetFileBytesAsync(string fileName, string directory)
        {
            var shareDirectory = _client.GetDirectoryClient($"{_fileShareSettings.BaseDirectory}/{directory}");
            var file = shareDirectory.GetFileClient(fileName);
            var downloadedFile = await file.DownloadAsync();
            using var stream = new MemoryStream();
            await downloadedFile.Value.Content.CopyToAsync(stream);

            return stream.ToArray();
        }

        public async Task<string> UploadFileAsync(string fileName, byte[] fileContent, string directory)
        {
            string[] arrayPath = directory.Split('/');
            var buildPath = _fileShareSettings.BaseDirectory;
            ShareDirectoryClient azureDirectory = _client.GetDirectoryClient($"{_fileShareSettings.BaseDirectory}");
            for (int i = 0; i < arrayPath.Length; i++)
            {
                buildPath += $"/{arrayPath[i]}";
                azureDirectory = _client.GetDirectoryClient(buildPath);
                await azureDirectory.CreateIfNotExistsAsync();
            }
            var file = azureDirectory.GetFileClient(fileName);
            using var stream = new MemoryStream(fileContent);
            file.Create(stream.Length);
            file.UploadRange(new HttpRange(0, stream.Length), stream);

            return fileName;
        }
    }
}
