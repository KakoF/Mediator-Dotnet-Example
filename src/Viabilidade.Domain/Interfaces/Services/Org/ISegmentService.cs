using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface ISegmentService
    {
        Task<IEnumerable<SegmentModel>> GetAsync(bool? active);
        Task<SegmentModel> GetAsync(int id);
    }
}