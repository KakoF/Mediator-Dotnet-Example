using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface IEntityService
    {
        Task<IEnumerable<EntityModel>> GetAsync(bool? active);
        Task<IEnumerable<EntityModel>> GetAllFilter(int? id, string name, string originalEntityId);
        Task<EntityModel> GetAsync(int id);
        Task<IEnumerable<EntityModel>> GetBySquadsAsync(IEnumerable<int> squadIds);
        Task<EntityModel> GetByOriginalEntityAsync(int originalEntityId);
        Task<IEnumerable<EntityModel>> GetBySegmentSquadAsync(int squadId, int segmentId);
        Task<EntityValidateDto> ValidateAsync(int segmentId, IEnumerable<string> entityIds);
        Task<IEnumerable<EntitySquadDto>> GetBySquadIdsAsync(IEnumerable<int> squadIds);
    }
}