using Dapper;
using Microsoft.AspNetCore.Http;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.ContextAccessor;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class RuleActiveRepository : UserContextAccessor, IRuleActiveRepository
    {
        private readonly IDbConnector _connector;
        public RuleActiveRepository(IDbConnector connector, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _connector = connector;
        }
        public async Task<RuleActiveModel> CreateAsync(RuleActiveModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.RegraAlertaAtiva" +
                "(RegraAlertaId, DataCriacao, UsuarioIdCriacao) OUTPUT Inserted.Id " +
                "VALUES" +
                "(@RegraAlertaId, @DataCriacao, @UsuarioIdCriacao)"
                , new { RegraAlertaId = entity.RuleId, DataCriacao = entity.CreateDate, UsuarioIdCriacao = _userId }, _connector.dbTransaction);
            return entity;
        }

        public async Task<bool> DeleteAsync(int ruleId)
        {
            var delete = await _connector.dbConnection.ExecuteAsync("DELETE FROM Alertas.RegraAlertaAtiva where RegraAlertaId = @ruleId", new { ruleId }, _connector.dbTransaction);
            return Convert.ToBoolean(delete);
        }

    }
}
