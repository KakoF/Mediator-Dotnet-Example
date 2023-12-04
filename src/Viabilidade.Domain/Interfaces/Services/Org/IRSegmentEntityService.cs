using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface IRSegmentEntityService
    {
        Task<RSegmentEntityModel> GetBySegmentEntityAsync(int segmentId, int entityId);
    }
}
