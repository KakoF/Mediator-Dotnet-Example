using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Repositories.Org
{
    public interface IRSegmentEntityRepository : IBaseRepository<RSegmentEntityModel>
    {
        Task<RSegmentEntityModel> GetBySegmentEntityAsync(int segmentId, int entityId);
    }
}
