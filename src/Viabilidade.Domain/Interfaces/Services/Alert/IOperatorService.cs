using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IOperatorService
    {
        Task<IEnumerable<OperatorModel>> GetAsync(bool? active);
        Task<OperatorModel> GetAsync(int id);
    }
}
