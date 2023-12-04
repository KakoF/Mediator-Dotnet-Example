using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Viabilidade.Domain.Interfaces.File;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Models.File;
using Viabilidade.Domain.Notifications;

namespace Viabilidade.Service.Services.File
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _azureContainerName;
        private readonly INotificationHandler<Notification> _notification;
        private readonly ILogger<FileService> _logger;
        public FileService(IConfiguration configuration, BlobServiceClient blobServiceClient, INotificationHandler<Notification> notification, ILogger<FileService> logger)
        {
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
            _azureContainerName = _configuration["AzureStorage:ContainerName"];
            _notification = notification;
            _logger = logger;
        }

        public async Task<List<string>> DeleteIfExistsAsync(IList<string> filesName)
        {
            try
            {
                List<string> deletedFiles = new List<string>();

                foreach (var name in filesName)
                {
                    BlobContainerClient cont = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
                    await cont.GetBlobClient(name).DeleteIfExistsAsync();

                    deletedFiles.Add(name);
                }

                return deletedFiles;
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<byte[]> GetAsync(string name)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_azureContainerName);
            try
            {
                var blobClient = blobContainer.GetBlobClient(name);
                var downloadContent = await blobClient.DownloadAsync();
                using (MemoryStream ms = new MemoryStream())
                {
                    await downloadContent.Value.Content.CopyToAsync(ms);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                _notification.AddNotification(404, $"Arquivo {name}, não encontrado");
                _logger.LogError(ex, $"Não foi possível recuperar arquivo {name}");
                return null;
            }
        }

        public async Task<List<FileStorageModel>> UploadAsync(FileModel file)
        {
            try
            {
                List<FileStorageModel> files = new List<FileStorageModel>();

                if (file.Files != null)
                {
                    foreach (var fileItem in file.Files)
                    {
                        string nomeArquivo = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileItem.FileName}";

                        FileStorageModel storage = new FileStorageModel()
                        {
                            Nome = nomeArquivo,
                            Caminho = $"{file.Tipo}/{(file.ObjetoId != null ? $"{file.ObjetoId}/" : "")}{(file.UsuarioId != null ? $"{file.UsuarioId}/" : "")}{nomeArquivo}",
                        };

                        var blobContainer = _blobServiceClient.GetBlobContainerClient(_azureContainerName);

                        var blobClient = blobContainer.GetBlobClient(storage.Caminho);

                        await blobClient.UploadAsync(fileItem.OpenReadStream());

                        files.Add(storage);
                    }
                }

                return files;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
