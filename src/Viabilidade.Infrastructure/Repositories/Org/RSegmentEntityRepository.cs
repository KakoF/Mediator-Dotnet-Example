using Dapper;
using Viabilidade.Domain.Models.Org;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Org
{
    public class RSegmentEntityRepository : BaseRepository<RSegmentEntityModel>, IRSegmentEntityRepository
    {
        private readonly IDbConnector _connector;
        protected override string _database => "Org.R_SegmentoEntidade";

        protected override string _selectCollumns => "Id, SegmentoId as SegmentId, EntidadeId as EntityId";

        public RSegmentEntityRepository(IDbConnector connector): base(connector)
        {
            _connector = connector;
        }

        public async Task<RSegmentEntityModel> GetBySegmentEntityAsync(int segmentId, int entityId)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<RSegmentEntityModel>($"Select {_selectCollumns} from {_database} where segmentoId = @segmentId and entidadeId = @entityId", new { segmentId, entityId }, _connector.dbTransaction);

        }
    }
}
