using Viabilidade.Domain.Models.Views;

namespace Viabilidade.Domain.Interfaces.Repositories.Org.Views
{
    public interface IVwChannelSquadEntityRepository
    {
        Task<IEnumerable<BondModel>> GetAsync(string search, int segmentId);
    }
}
