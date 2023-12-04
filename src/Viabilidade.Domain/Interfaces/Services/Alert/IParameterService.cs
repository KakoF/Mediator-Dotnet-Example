using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IParameterService
    {
        Task<ParameterModel> CreateAsync(ParameterModel model);
        Task<ParameterModel> UpdateAsync(int id, ParameterModel model);
        Task<IEnumerable<ParameterModel>> GetAsync(bool? active);
        Task<ParameterModel> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
