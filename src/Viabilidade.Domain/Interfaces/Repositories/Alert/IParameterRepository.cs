using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IParameterRepository : IBaseRepository<ParameterModel>
    {
        Task<ParameterModel> CreateAsync(ParameterModel entity);

        Task<ParameterModel> UpdateAsync(int id, ParameterModel entity);

        Task<bool> DeleteAsync(int id);
    }
}