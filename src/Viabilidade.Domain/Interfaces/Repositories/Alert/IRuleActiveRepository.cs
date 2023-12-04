using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IRuleActiveRepository
    {
        Task<RuleActiveModel> CreateAsync(RuleActiveModel entity);
        Task<bool> DeleteAsync(int ruleId);
        
    }
}
