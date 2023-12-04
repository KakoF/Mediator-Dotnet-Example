using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.QueryParams.Treatment;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface ITreatmentRepository
    {
        Task<TreatmentModel> CreateAsync(TreatmentModel entity);
        Task<IEnumerable<Tuple<TreatmentGroupDto, int>>> GroupAsync(TreatmentQueryParams queryParams);
        Task<TreatmentModel> PreviewAsync(int id);
        Task<TreatmentModel> PreviewDetailAsync(int id);
        Task<IEnumerable<TreatmentByEntityRuleGroupDto>> GetByEntityRuleGroupAsync(int entityRuleId);
        Task<TreatmentModel> GetAsync(int id);
        Task<int> CountByEntityRuleWasProblemGroupAsync(int entityRuleId);
    }
}
