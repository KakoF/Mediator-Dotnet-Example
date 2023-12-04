using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Repositories.Org
{
    public interface IEntityRepository : IBaseRepository<EntityModel>
    {
        Task<EntityModel> GetByIdAsync(int id);
        Task<EntityModel> GetByOriginalEntityAsync(int originalEntityId);
        Task<IEnumerable<EntityModel>> GetAllFilter(int? id, string name, string originalEntityId);
        Task<IEnumerable<EntityModel>> GetBySegmentSquadAsync(int squadId, int segmentId);
        Task<IEnumerable<EntitySquadDto>> GetBySquadIdsAsync(IEnumerable<int> squadIds);
        Task<IEnumerable<EntityModel>> GetBySquadsAsync(IEnumerable<int> squadIds);
        Task<IEnumerable<EntityModel>> GetByOriginalEntityAsync(IEnumerable<int> originalEntityIds);
        Task<IEnumerable<EntitySquadDto>> GetByOriginalIdsAsync(IEnumerable<int> originalEntityIds);
    }
}