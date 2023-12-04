using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface ITagService
    {
        Task<IEnumerable<TagModel>> GetAsync(bool? active);
        Task<TagModel> GetAsync(int id);
    }
}
