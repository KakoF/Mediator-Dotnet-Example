using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Service.Services.Org
{
    public class RSegmentEntityService : IRSegmentEntityService
    {

        private readonly IRSegmentEntityRepository _rSegmentEntityRepository;
        public RSegmentEntityService(IRSegmentEntityRepository rSegmentEntityRepository)
        {
            _rSegmentEntityRepository = rSegmentEntityRepository;
        }
        public async Task<RSegmentEntityModel> GetBySegmentEntityAsync(int segmentId, int entityId)
        {
            return await _rSegmentEntityRepository.GetBySegmentEntityAsync(segmentId, entityId);
        }
    }
}
