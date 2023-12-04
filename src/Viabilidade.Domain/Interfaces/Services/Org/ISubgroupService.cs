using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface ISubgroupService
    {
        Task<IEnumerable<SubgroupModel>> GetAsync(bool? active);
        Task<SubgroupModel> GetAsync(int id);
    }
}