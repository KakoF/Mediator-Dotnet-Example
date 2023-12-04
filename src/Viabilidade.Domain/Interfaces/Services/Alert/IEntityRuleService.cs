using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams;
using Viabilidade.Domain.Models.QueryParams.EntityRule;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IEntityRuleService
    {
        Task<EntityRuleModel> GetAsync(int id);
        Task<IEnumerable<EntityRuleModel>> GetByRuleAsync(int ruleId);
        Task<IEnumerable<EntityRuleModel>> GetByEntityAsync(int entityId);
        Task<PaginationModel<EntityRuleGroupByRuleDto>> GroupByRuleAsync(int ruleId, EntityRuleQueryParams queryParams);
        Task<PaginationModel<EntityRuleGroupByEntityDto>> GroupByEntityAsync(int entityId, EntityRuleQueryParams queryParams);
        Task<EntityRuleModel> CreateAsync(EntityRuleModel entity);
        Task<EntityRuleModel> PreviewAsync(int id);
        Task<EntityRuleModel> PreviewAsync(int regraId, int entidadeId);
        Task<EntityRuleModel> UpdateAsync(int id, EntityRuleModel entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByRuleAsync(int ruleId);
        Task<(bool, string)> RuleEntityApplicant(int entityRuleId, int alertsQuantity, int treatmentsQuantity, decimal percentageTreatment);
    }
}