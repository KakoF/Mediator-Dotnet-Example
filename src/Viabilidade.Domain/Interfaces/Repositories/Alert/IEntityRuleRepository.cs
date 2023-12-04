using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.QueryParams;
using Viabilidade.Domain.Models.QueryParams.EntityRule;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IEntityRuleRepository
    {
        Task<EntityRuleModel> CreateAsync(EntityRuleModel entity);
        Task<EntityRuleModel> UpdateAsync(int id, EntityRuleModel entity);
        Task<EntityRuleModel> GetAsync(int id);
        Task<EntityRuleModel> PreviewAsync(int id);
        Task<EntityRuleModel> PreviewAsync(int ruleId, int entityId);
        Task<IEnumerable<EntityRuleModel>> GetByRuleAsync(int ruleId);
        Task<IEnumerable<EntityRuleModel>> GetByEntityAsync(int entityId);
        Task<IEnumerable<Tuple<EntityRuleGroupByRuleDto, int>>> GroupByRuleAsync(int ruleId, EntityRuleQueryParams queryParams);
        Task<IEnumerable<Tuple<EntityRuleGroupByEntityDto, int>>> GroupByEntityAsync(int entityId, EntityRuleQueryParams queryParams);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByRuleAsync(int ruleId);
        Task<IEnumerable<int>> GetIdsByRuleAsync(int ruleId);
        Task<IEnumerable<int>> GetIdsByEntityAsync(int entityId);
    }
}
