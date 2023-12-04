using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Treatment;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface ITreatmentService
    {
        Task<TreatmentModel> CreateAsync(TreatmentModel model);
        Task<PaginationModel<TreatmentGroupDto>> GroupAsync(TreatmentQueryParams queryParams);
        Task<IEnumerable<TreatmentByEntityRuleGroupDto>> GetByEntityRuleGroupAsync(int entityRuleId);
        Task<TreatmentModel> GetAsync(int id);
        Task<TreatmentPreviewDto> PreviewAsync(int id);
        Task<TreatmentGroupPreviewDto> GroupPreviewAsync(int id, int entityRuleId);
    }
}
