using Viabilidade.Domain.Models.Views;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface IBondService
    {
        Task<IEnumerable<BondModel>> GetAsync(string search, int segmentId);
    }
}