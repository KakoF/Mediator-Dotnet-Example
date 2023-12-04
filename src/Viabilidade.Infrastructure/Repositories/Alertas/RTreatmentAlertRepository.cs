using Dapper;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class RTreatmentAlertRepository : IRTreatmentAlertRepository
    {
        private readonly IDbConnector _connector;
        public RTreatmentAlertRepository(IDbConnector connector)
        {
            _connector = connector;
        }
        public async Task<RTreatmentAlertModel> CreateAsync(RTreatmentAlertModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.R_AlertaGeradoTratativa " +
                "(AlertaGeradoId, AlertaGeradoTratativaId) OUTPUT Inserted.* " +
                "VALUES " +
                "(@AlertaGeradoId, @AlertaGeradoTratativaId) "
                , new { AlertaGeradoId = entity.AlertId, AlertaGeradoTratativaId = entity.TreatmentId }, _connector.dbTransaction);
            return entity;
        }
    }
}
