using Dapper;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class TreatmentTypeRepository : BaseRepository<TreatmentTypeModel>, ITreatmentTypeRepository
    {
        private readonly IDbConnector _connector;
        protected override string _database => "Alertas.TipoTratativa";

        protected override string _selectCollumns => "Id, Descricao as Description, ClasseTratativaId as TreatmentClassId, TipoTratativaConceito as TreatmentTypeConcept, Ativo as Active";

        public TreatmentTypeRepository(IDbConnector connector): base(connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<TreatmentTypeModel>> GetByTreatmentClassAsync(int treatmentClassId, bool? active = null)
        {
            return await _connector.dbConnection.QueryAsync<TreatmentTypeModel>($"Select {_selectCollumns} from {_database} where classeTratativaId = @treatmentClassId {(active != null ? "and ativo = @active" : "")}", new { treatmentClassId, active }, _connector.dbTransaction);
        }
    }
}
