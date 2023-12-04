using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IRuleActiveService
    {
        Task<RuleActiveModel> CreateAsync(RuleActiveModel model);
        Task<bool> DeleteAsync(int ruleId);
    }
}
