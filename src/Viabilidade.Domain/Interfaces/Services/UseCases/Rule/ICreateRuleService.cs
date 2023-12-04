using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.UseCases.Rule
{
    public interface ICreateRuleService
    {
        Task<RuleModel> CreateAsync(RuleModel rule);
    }
}
