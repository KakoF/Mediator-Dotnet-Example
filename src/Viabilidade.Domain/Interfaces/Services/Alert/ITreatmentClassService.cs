using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface ITreatmentClassService
    {
        Task<IEnumerable<TreatmentClassModel>> GetAsync(bool? active);
        Task<TreatmentClassModel> GetAsync(int id);
    }
}
