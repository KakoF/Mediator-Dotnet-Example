using Viabilidade.Domain.DTO.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.QueryParams.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IAlertRepository
    {
        Task<AlertModel> CreateAsync(AlertModel entity);
        Task<IEnumerable<AlertModel>> GetAsync();
        Task<IEnumerable<AlertModel>> GetByEntityRuleAsync(int entityRuleId, bool? treated);
        Task<AlertModel> GetLastByEntityRuleAsync(int entityRuleId);
        Task<IEnumerable<Tuple<AlertGroupDto, int>>> GroupAsync(AlertQueryParams queryParams);
        Task<AlertModel> GetAsync(int id);
        Task<AlertModel> PreviewAsync(int id);
        Task<IEnumerable<AlertModel>> GetAsync(IEnumerable<int> ids);
        Task<AlertModel> UpdateAsync(int id, AlertModel entity);
        Task<bool> DeleteAsync(int id);
        Task<int> CountLowSeverityByEntityRuleAsync(int entityRuleId);
        Task ExeuteProcAsync();
    }
}
