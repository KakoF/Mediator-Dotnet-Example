using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IIndicatorRepository : IBaseRepository<IndicatorModel>
    {
        Task<IEnumerable<IndicatorModel>> GetBySegmentAsync(int segmentId, bool? active = null);
    }
}
