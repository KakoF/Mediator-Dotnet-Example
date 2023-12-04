using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IIndicatorFilterService
    {
        Task<IEnumerable<IndicatorFilterModel>> GetAsync(bool? active);
        Task<IndicatorFilterModel> GetAsync(int id);
    }
}
