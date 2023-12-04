using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface ITreatmentTypeRepository : IBaseRepository<TreatmentTypeModel>
    {
        Task<IEnumerable<TreatmentTypeModel>> GetByTreatmentClassAsync(int treatmentClassId, bool? active = null);
    }
}
