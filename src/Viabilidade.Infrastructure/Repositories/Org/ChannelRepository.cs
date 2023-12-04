using Dapper;
using Viabilidade.Domain.Models.Org;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Org
{
    public class ChannelRepository : BaseRepository<ChannelModel>, IChannelRepository
    {
        private readonly IDbConnector _connector;
        protected override string _database => "Org.Canal";

        protected override string _selectCollumns => "Id, Nome as Name, Ativo as Active, CanalIdOriginal as OriginalChannelId, TipoSubGrupoId as SubgroupId";

        public ChannelRepository(IDbConnector connector): base(connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<ChannelModel>> GetBySubgroupAsync(int subgroupId, bool? active = null)
        {
            return await _connector.dbConnection.QueryAsync<ChannelModel>($"Select {_selectCollumns} from {_database} where tipoSubGrupoId = @subgroupId {(active != null ? "and ativo = @active" : "")}", new { subgroupId, active }, _connector.dbTransaction);
        }
    }
}
