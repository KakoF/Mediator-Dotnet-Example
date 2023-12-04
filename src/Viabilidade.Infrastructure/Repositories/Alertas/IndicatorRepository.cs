using Dapper;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class IndicatorRepository : BaseRepository<IndicatorModel>, IIndicatorRepository
    {
        private readonly IDbConnector _connector;
        protected override string _database => "Alertas.Indicador";

        protected override string _selectCollumns => "Id, Descricao as Description, Comando as Command, Ativo as Active, IndicadorSQL as SQLIndicator, SegmentoId as SegmentId";

        public IndicatorRepository(IDbConnector connector): base(connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<IndicatorModel>> GetBySegmentAsync(int segmentId, bool? active = null)
        {
            return await _connector.dbConnection.QueryAsync<IndicatorModel>($"Select {_selectCollumns} from {_database} where segmentoId = @segmentId {(active != null ? "and ativo = @active" : "")}", new { segmentId, active }, _connector.dbTransaction);
        }
    }
}
