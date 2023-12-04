using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Models.Org;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Org
{
    public class SubgroupRepository : BaseRepository<SubgroupModel>, ISubgroupRepository
    {
        protected override string _database => "Org.TipoSubGrupo";

        protected override string _selectCollumns => "Id, Nome as Name, Ativo As Active";

        public SubgroupRepository(IDbConnector connector) : base(connector)
        {
        }

    }
}
