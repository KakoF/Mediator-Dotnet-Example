using Dapper;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class ParameterRepository : BaseRepository<ParameterModel>, IParameterRepository
    {
        private readonly IDbConnector _connector;
        protected override string _database => "Alertas.Parametro";

        protected override string _selectCollumns => "Id, Ativo as Active, CriticidadeBaixa as LowSeverity, CriticidadeMedia as MediumSeverity, CriticidadeAlta as HighSeverity, PeriodoIndicador as EvaluationPeriod, PeriodoHistorico as ComparativePeriod";

        public ParameterRepository(IDbConnector connector) : base(connector)
        {
            _connector = connector;
        }

        public async Task<ParameterModel> CreateAsync(ParameterModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.Parametro" +
                "(Ativo, CriticidadeBaixa, CriticidadeMedia, CriticidadeAlta, PeriodoIndicador, PeriodoHistorico) OUTPUT Inserted.Id " +
                "VALUES" +
                "(@Ativo, @CriticidadeBaixa, @CriticidadeMedia, @CriticidadeAlta, @PeriodoIndicador, @PeriodoHistorico) "
                , new { Ativo = entity.Active, CriticidadeBaixa = entity.LowSeverity, CriticidadeMedia = entity.MediumSeverity, CriticidadeAlta = entity.HighSeverity, PeriodoIndicador = entity.EvaluationPeriod, PeriodoHistorico = entity.ComparativePeriod }, _connector.dbTransaction);
            return entity;
        }

        public async Task<ParameterModel> UpdateAsync(int id, ParameterModel entity)
        {
            entity.Id = id;
            await _connector.dbConnection.ExecuteAsync("UPDATE Alertas.Parametro " +
              "SET Ativo=@Ativo, CriticidadeBaixa=@CriticidadeBaixa, CriticidadeMedia=@CriticidadeMedia, CriticidadeAlta=@CriticidadeAlta, PeriodoIndicador=@PeriodoIndicador, PeriodoHistorico=@PeriodoHistorico " +
              "where id = @id", new { id, Ativo = entity.Active, CriticidadeBaixa = entity.LowSeverity, CriticidadeMedia = entity.MediumSeverity, CriticidadeAlta = entity.HighSeverity, PeriodoIndicador = entity.EvaluationPeriod, PeriodoHistorico = entity.ComparativePeriod }, _connector.dbTransaction);
            return entity;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var delete = await _connector.dbConnection.ExecuteAsync("DELETE FROM Alertas.Parametro where Id = @id", new { id }, _connector.dbTransaction);
            return Convert.ToBoolean(delete);
        }
    }
}
