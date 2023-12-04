using Viabilidade.Domain.DTO.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IAlertService
    {
        Task<AlertModel> CreateAsync(AlertModel model);
        Task<IEnumerable<AlertModel>> GetAsync();
        Task<IEnumerable<AlertModel>> GetByEntityRuleAsync(int entityRuleId, bool? treated);
        Task<PaginationModel<AlertGroupDto>> GroupAsync(AlertQueryParams queryParams);
        Task<AlertModel> GetAsync(int id);
        Task<IEnumerable<AlertModel>> GetAsync(IEnumerable<int> ids);
        Task<AlertModel> PreviewAsync(int id);
        Task<AlertModel> UpdateAsync(int id, AlertModel model);
        Task<bool> DeleteAsync(int id);
        Task<AlertModel> UpdateUserAsync(int id, AlertModel updateModel);
        Task GenerateAsync(int entityRuleId, int quantity);
    }
}
