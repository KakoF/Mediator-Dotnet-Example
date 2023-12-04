using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.UseCases.Rule
{
    public interface IUpdateRuleService
    {
        Task<RuleModel> UpdateAsync(int ruleId, RuleModel rule);
    }
}
