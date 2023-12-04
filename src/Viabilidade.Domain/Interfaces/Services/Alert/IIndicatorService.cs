using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IIndicatorService
    {
        Task<IEnumerable<IndicatorModel>> GetAsync(bool? active);
        Task<IndicatorModel> GetAsync(int id);
        Task<IEnumerable<IndicatorModel>> GetBySegmentAsync(int segmentId, bool? active);
    }
}
