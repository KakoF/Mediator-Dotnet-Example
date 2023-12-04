using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface ITreatmentTypeService
    {
        Task<IEnumerable<TreatmentTypeModel>> GetAsync(bool? active);
        Task<TreatmentTypeModel> GetAsync(int id);
        Task<IEnumerable<TreatmentTypeModel>> GetByTreatmentClassAsync(int treatmentClassId, bool? active);
    }
}
