using Viabilidade.Domain.Interfaces.Repositories.Org.Views;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Views;

namespace Viabilidade.Service.Services.Org
{
    public class BondService : IBondService
    {
        private readonly IVwChannelSquadEntityRepository _vwCanalSquadEntidadeRepository;
        public BondService(IVwChannelSquadEntityRepository vwCanalSquadEntidadeRepository)
        {
            _vwCanalSquadEntidadeRepository = vwCanalSquadEntidadeRepository;
        }
        public async Task<IEnumerable<BondModel>> GetAsync(string search, int segmentId)
        {
           return  await _vwCanalSquadEntidadeRepository.GetAsync(search, segmentId);
        }
    }
}
