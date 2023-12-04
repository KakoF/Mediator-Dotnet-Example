using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAsync(bool? active = null);
        Task<T> GetAsync(int id);
    }
}
