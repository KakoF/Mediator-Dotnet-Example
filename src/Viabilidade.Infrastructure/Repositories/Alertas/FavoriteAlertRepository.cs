using Dapper;
using Microsoft.AspNetCore.Http;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.ContextAccessor;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class FavoriteAlertRepository : UserContextAccessor, IFavoriteAlertRepository
    {
        private readonly IDbConnector _connector;
        public FavoriteAlertRepository(IDbConnector connector, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _connector = connector;
        }

        public async Task<FavoriteAlertModel> GetAsync(int id)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<FavoriteAlertModel>("Select Id, UsuarioId as UserId, AlertaId as RuleId, DataEdicao as UpdateDate, Ativo as Active from Alertas.Alertafavorito where id = @id", new { id }, _connector.dbTransaction);
        }

        public async Task<FavoriteAlertModel> GetByRuleUserAsync(int ruleId)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<FavoriteAlertModel>("Select Id, UsuarioId as UserId, AlertaId as RuleId, DataEdicao as UpdateDate, Ativo as Active from Alertas.Alertafavorito where AlertaId = @ruleId and UsuarioId = @UsuarioId order by id desc", new { ruleId, UsuarioId = _userId }, _connector.dbTransaction);
        }

        public async Task<FavoriteAlertModel> CreateAsync(FavoriteAlertModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.Alertafavorito " +
                "(UsuarioId, AlertaId, DataEdicao, Ativo) OUTPUT Inserted.Id " +
                "VALUES " +
                "(@UsuarioId, @AlertaId, @DataEdicao, @Ativo)"
                , new { UsuarioId = _userId, AlertaId = entity.RuleId, DataEdicao = entity.UpdateDate, Ativo = entity.Active }, _connector.dbTransaction);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return Convert.ToBoolean(await _connector.dbConnection.ExecuteAsync("DELETE FROM Alertas.Alertafavorito where Id = @id and UsuarioId = @UsuarioId", new { id, UsuarioId = _userId }, _connector.dbTransaction));
        }

        public async Task<FavoriteAlertModel> UpdateAsync(int id, FavoriteAlertModel entity)
        {
            entity.Id = id;
            await _connector.dbConnection.ExecuteAsync("UPDATE Alertas.Alertafavorito " +
              "SET UsuarioId=@UsuarioId, AlertaId=@AlertaId, DataEdicao=@DataEdicao, Ativo=@Ativo " +
              "where id = @id", new { id, UsuarioId = _userId, AlertaId = entity.RuleId, DataEdicao = entity.UpdateDate, Ativo = entity.Active }, _connector.dbTransaction);
            return entity;
        }

        public async Task<bool> ExistFavoriteAsync(int ruleId)
        {
            var entity = await _connector.dbConnection.QueryFirstOrDefaultAsync<FavoriteAlertModel>("Select Id, UsuarioId, AlertaId, DataEdicao, Ativo from Alertas.Alertafavorito where AlertaId = @ruleId and UsuarioId = @UsuarioId and Ativo = 1", new { ruleId, UsuarioId = _userId }, _connector.dbTransaction);
            if (entity == null)
                return false;

            return true;

        }
    }
}
