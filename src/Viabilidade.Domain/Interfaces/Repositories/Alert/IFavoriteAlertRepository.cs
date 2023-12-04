using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IFavoriteAlertRepository
    {
        Task<FavoriteAlertModel> GetAsync(int id);
        Task<FavoriteAlertModel> GetByRuleUserAsync(int ruleId);
        Task<bool> ExistFavoriteAsync(int ruleId);
        Task<FavoriteAlertModel> CreateAsync(FavoriteAlertModel entity);
        Task<FavoriteAlertModel> UpdateAsync(int id, FavoriteAlertModel entity);
        Task<bool> DeleteAsync(int id);
    }
}
