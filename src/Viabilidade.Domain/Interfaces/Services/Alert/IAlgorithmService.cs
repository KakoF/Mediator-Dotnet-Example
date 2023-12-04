using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IAlgorithmService
    {
        Task<IEnumerable<AlgorithmModel>> GetAsync(bool? active);
        Task<AlgorithmModel> GetAsync(int id);
    }
}
