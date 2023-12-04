using Dapper;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class SilencedAlertRepository : ISilencedAlertRepository
    {
        private readonly IDbConnector _connector;
        public SilencedAlertRepository(IDbConnector connector)
        {
            _connector = connector;
        }
        public async Task<SilencedAlertModel> CreateAsync(SilencedAlertModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.AlertaGeradoSilenciado" +
                "(AlertaGeradoId, Datainiciosilenciar, Datafimsilenciar, Ativo) OUTPUT Inserted.Id " +
                "VALUES" +
                "(@AlertaGeradoId, @DataInicioSilenciar, @DataFimSilenciar, @Ativo)"
                , new { AlertaGeradoId = entity.AlertId, DataInicioSilenciar = entity.StartDateSilence, DataFimSilenciar = entity.EndDateSilence, Ativo = entity.Active }, _connector.dbTransaction);
            return entity;
        }

    }
}
