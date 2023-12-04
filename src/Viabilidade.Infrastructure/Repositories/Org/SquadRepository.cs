using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Models.Org;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Org
{
    public class SquadRepository : BaseRepository<SquadModel>, ISquadRepository
    {
        protected override string _database => "Org.Squad";

        protected override string _selectCollumns => "Id, Nome As Name, Ativo As Active";

        public SquadRepository(IDbConnector connector) : base(connector)
        {
        }

    }
}
