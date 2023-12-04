using Viabilidade.Domain.DTO.Rule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Rule;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IRuleService
    {
        Task<RuleModel> CreateAsync(RuleModel entity);
        Task<PaginationModel<RuleGroupDto>> GroupAsync(RuleQueryParams queryParams);
        Task<IEnumerable<RuleModel>> GetAsync();
        Task<RuleModel> GetAsync(int id);
        Task<RuleModel> PreviewAsync(int id);
        Task<RuleModel> PreviewRuleActiveAsync(int id);
        Task<RuleModel> UpdateAsync(int id, RuleModel entity);
        Task<bool> DeleteAsync(int id);
        Task<RuleModel> ActiveInactiveAsync(int id, bool active);
        Task SetRootRuleToRulesAsync();
    }
}