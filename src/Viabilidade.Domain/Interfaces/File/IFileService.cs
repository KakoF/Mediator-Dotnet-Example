using Viabilidade.Domain.Models.File;

namespace Viabilidade.Domain.Interfaces.File
{
    public interface IFileService
    {
        Task<List<FileStorageModel>> UploadAsync(FileModel file);
        Task<byte[]> GetAsync(string name);
        Task<List<string>> DeleteIfExistsAsync(IList<string> filesName);
    }
}
