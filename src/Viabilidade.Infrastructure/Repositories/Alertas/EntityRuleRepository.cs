using Dapper;
using Microsoft.AspNetCore.Http;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Infrastructure.ContextAccessor;
using Viabilidade.Infrastructure.Interfaces.DataConnector;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Models.QueryParams.EntityRule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class EntityRuleRepository : UserContextAccessor, IEntityRuleRepository
    {
        private readonly IDbConnector _connector;
        public EntityRuleRepository(IDbConnector connector, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _connector = connector;
        }
        public async Task<EntityRuleModel> CreateAsync(EntityRuleModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.RegraEntidade" +
               "(AlertaId, EntidadeId, NmSubEntidade, UsuarioIdResponsavel, FiltroId, ParametroId, Ativo) OUTPUT Inserted.Id " +
               "VALUES" +
               "(@AlertaId, @EntidadeId, @NmSubEntidade, @UsuarioIdResponsavel, @FiltroId, @ParametroId, @Ativo) "
               , new { AlertaId = entity.RuleId, EntidadeId = entity.EntityId, NmSubEntidade = entity.SubEntityNumber, UsuarioIdResponsavel = _userId, FiltroId = entity.IndicatorFilterId, ParametroId = entity.ParameterId, Ativo = entity.Active }, _connector.dbTransaction);
            return entity;

        }

        public async Task<EntityRuleModel> UpdateAsync(int id, EntityRuleModel entity)
        {
            entity.Id = id;
            await _connector.dbConnection.ExecuteAsync("UPDATE Alertas.RegraEntidade " +
               "SET AlertaId = @AlertaId, EntidadeId = @EntidadeId, NmSubEntidade = @NmSubEntidade, UsuarioIdResponsavel = @UsuarioIdResponsavel, FiltroId = @FiltroId, ParametroId = @ParametroId, Ativo = @Ativo " +
               "where id = @id", new { id, AlertaId = entity.RuleId, EntidadeId = entity.EntityId, NmSubEntidade = entity.SubEntityNumber, UsuarioIdResponsavel = _userId, FiltroId = entity.IndicatorFilterId, ParametroId = entity.ParameterId, Ativo = entity.Active }, _connector.dbTransaction);
            return entity;
        }

        public async Task<EntityRuleModel> GetAsync(int id)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<EntityRuleModel>($"Select Id, AlertaId as RuleId, EntidadeId as EntityId, NmSubEntidade as SubEntityNumber, UsuarioIdResponsavel as UserId, FiltroId as IndicatorFilterId, ParametroId as ParameterId, Ativo as Active from Alertas.RegraEntidade where id = @id", new { id }, _connector.dbTransaction);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delete = await _connector.dbConnection.ExecuteAsync("DELETE FROM Alertas.RegraEntidade where Id = @id", new { id }, _connector.dbTransaction);
            return Convert.ToBoolean(delete);
        }

        public async Task<bool> DeleteByRuleAsync(int ruleId)
        {
            var delete = await _connector.dbConnection.ExecuteAsync("DELETE FROM Alertas.RegraEntidade where AlertaId = @ruleId", new { ruleId }, _connector.dbTransaction);
            return Convert.ToBoolean(delete);
        }

        public async Task<EntityRuleModel> PreviewAsync(int id)
        {
            var channels = new List<ChannelModel>();
            var rules = await _connector.dbConnection.QueryAsync<EntityRuleModel, RuleModel, EntityModel, IndicatorFilterModel, ParameterModel, ChannelModel, EntityRuleModel>(
                "Select re.Id, re.AlertaId as RuleId, re.EntidadeId as EntityId, re.NmSubEntidade as SubEntityNumber, re.UsuarioIdResponsavel as UserId, re.FiltroId as IndicatorFilterId, re.ParametroId as ParameterId, re.Ativo as Active, " +
                "ra.Id, ra.Nome as Name, ra.Observacao as Description, ra.TipoId as AlgorithmId, ra.IndicadorId as IndicatorId, ra.OperadorId as OperatorId, ra.FiltroId as IndicatorFilterId, ra.ParametroId as ParameterId, ra.Ativo as Active, ra.DataUltimaAlteracao as LastUpdateDate, ra.UsuarioIdCriacao as UserId, ra.VersaoMajor, ra.VersaoMinor, ra.VersaoPatch, " +
                "e.Id, e.Nome as Name, e.Ativo as Active, e.EntidadeIdOriginal as OriginalEntityId, e.SegmentoId as SegmentId, " +
                "fi.Id, fi.Descricao as Description, fi.Comando as Command, fi.Ativo as Active, " +
                "pa.Id, pa.Ativo as Active, pa.CriticidadeBaixa as LowSeverity, pa.CriticidadeMedia as MediumSeverity, pa.CriticidadeAlta as HighSeverity, pa.PeriodoIndicador as EvaluationPeriod, pa.PeriodoHistorico as ComparativePeriod, " +
                "c.Id, c.Nome as Name, c.Ativo as Active, c.CanalIdOriginal as OriginalChannelId, c.TipoSubGrupoId as SubgroupId " +
                "from Alertas.RegraEntidade re " +
                "inner join Alertas.RegraAlerta ra on ra.Id = re.AlertaId " +
                "inner join Org.Entidade e on e.Id = re.EntidadeId " +
                "left join Alertas.FiltroIndicador fi on fi.Id = re.FiltroId " +
                "left join Alertas.Parametro pa on pa.Id = re.ParametroId " +
                "left join Alertas.R_RegraEntidadeCanal R_ec on R_ec.RegraEntidadeId = re.Id " +
                "left join Org.Canal c on c.Id = R_ec.CanalId " +
                "where re.Id = @id",
                map: (entityRule, rule, entity, indicatorFilter, parameter, channel) =>
                {
                    entityRule.Rule = rule;
                    entityRule.Entity = entity;

                    if (channel != null)
                    {
                        channels.Add(channel);
                        entityRule.Channels = channels;
                        channels = new List<ChannelModel>();
                    }

                    if (indicatorFilter != null)
                        entityRule.IndicatorFilter = indicatorFilter;

                    if (parameter != null)
                        entityRule.Parameter = parameter;

                    return entityRule;
                },
                param: new { id },
                _connector.dbTransaction,
               splitOn: "Id, Id, Id, Id, Id");
            return rules.FirstOrDefault();
        }



        public async Task<EntityRuleModel> PreviewAsync(int ruleId, int entityId)
        {
            var channels = new List<ChannelModel>();
            var rules = await _connector.dbConnection.QueryAsync<EntityRuleModel, RuleModel, EntityModel, IndicatorFilterModel, ParameterModel, ChannelModel, EntityRuleModel>(
                "Select re.Id, re.AlertaId as RuleId, re.EntidadeId as EntityId, re.NmSubEntidade as SubEntityNumber, re.UsuarioIdResponsavel as UserId, re.FiltroId as IndicatorFilterId, re.ParametroId as ParameterId, re.Ativo as Active, " +
                "ra.Id, ra.Nome as Name, ra.Observacao as Description, ra.TipoId as AlgorithmId, ra.IndicadorId as IndicatorId, ra.OperadorId as OperatorId, ra.FiltroId as IndicatorFilterId, ra.ParametroId as ParameterId, ra.Ativo as Active, ra.DataUltimaAlteracao as LastUpdateDate, ra.UsuarioIdCriacao as UserId, ra.VersaoMajor, ra.VersaoMinor, ra.VersaoPatch, " +
                "e.Id, e.Nome as Name, e.Ativo as Active, e.EntidadeIdOriginal as OriginalEntityId, e.SegmentoId as SegmentId, " +
                "fi.Id, fi.Descricao as Description, fi.Comando as Command, fi.Ativo as Active, " +
                "pa.Id, pa.Ativo as Active, pa.CriticidadeBaixa as LowSeverity, pa.CriticidadeMedia as MediumSeverity, pa.CriticidadeAlta as HighSeverity, pa.PeriodoIndicador as EvaluationPeriod, pa.PeriodoHistorico as ComparativePeriod, " +
                "c.Id, c.Nome as Name, c.Ativo as Active, c.CanalIdOriginal as OriginalChannelId, c.TipoSubGrupoId as SubgroupId " +
                "from Alertas.RegraEntidade re " +
                "inner join Alertas.RegraAlerta ra on ra.Id = re.AlertaId " +
                "inner join Org.Entidade e on e.Id = re.EntidadeId " +
                "left join Alertas.FiltroIndicador fi on fi.Id = re.FiltroId " +
                "left join Alertas.Parametro pa on pa.Id = re.ParametroId " +
                "where re.AlertaId = @ruleId and re.EntidadeId = @entityId",
                map: (entityRule, rule, entity, indicatorFilter, parameter, channel) =>
                {
                    entityRule.Rule = rule;
                    entityRule.Entity = entity;

                    if (channel != null)
                    {
                        channels.Add(channel);
                        entityRule.Channels = channels;
                        channels = new List<ChannelModel>();
                    }

                    if (indicatorFilter != null)
                        entityRule.IndicatorFilter = indicatorFilter;

                    if (parameter != null)
                        entityRule.Parameter = parameter;

                    return entityRule;
                },
                param: new { ruleId, entityId },
                _connector.dbTransaction,
               splitOn: "Id, Id, Id, Id, Id");
            return rules.FirstOrDefault();
        }

        public async Task<IEnumerable<int>> GetIdsByRuleAsync(int ruleId)
        {
            return await _connector.dbConnection.QueryAsync<int>("Select re.Id from Alertas.RegraEntidade re where re.AlertaId = @ruleId", new { ruleId }, _connector.dbTransaction);

        }

        public async Task<IEnumerable<int>> GetIdsByEntityAsync(int entityId)
        {
            return await _connector.dbConnection.QueryAsync<int>("Select re.Id from Alertas.RegraEntidade re where re.EntidadeId = @entityId", new { entityId }, _connector.dbTransaction);
        }

        public async Task<IEnumerable<EntityRuleModel>> GetByRuleAsync(int ruleId)
        {
            var channels = new List<ChannelModel>();
            return await _connector.dbConnection.QueryAsync<EntityRuleModel, EntityModel, IndicatorFilterModel, ParameterModel, ChannelModel, EntityRuleModel>($"Select re.Id, re.AlertaId as RuleId, re.EntidadeId as EntityId, re.NmSubEntidade as SubEntityNumber, re.UsuarioIdResponsavel as UserId, re.FiltroId as IndicatorFilterId, re.ParametroId as ParameterId, re.Ativo as Active, " +
               "e.Id, e.Nome as Name, e.Ativo as Active, e.EntidadeIdOriginal as OriginalEntityId, e.SegmentoId as SegmentId, " +
                "fi.Id, fi.Descricao as Description, fi.Comando as Command, fi.Ativo as Active, " +
                "pa.Id, pa.Ativo as Active, pa.CriticidadeBaixa as LowSeverity, pa.CriticidadeMedia as MediumSeverity, pa.CriticidadeAlta as HighSeverity, pa.PeriodoIndicador as EvaluationPeriod, pa.PeriodoHistorico as ComparativePeriod, " +
                "c.Id, c.Nome as Name, c.Ativo as Active, c.CanalIdOriginal as OriginalChannelId, c.TipoSubGrupoId as SubgroupId " +
                "from Alertas.RegraEntidade re " +
                "inner join Org.Entidade e on e.Id = re.EntidadeId " +
                "left join Alertas.FiltroIndicador fi on fi.Id = re.FiltroId " +
                "left join Alertas.Parametro pa on pa.Id = re.ParametroId " +
                "left join Alertas.R_RegraEntidadeCanal R_ec on R_ec.RegraEntidadeId = re.Id " +
                "left join Org.Canal c on c.Id = R_ec.CanalId " +
                "where re.AlertaId = @ruleId",
                map: (entityRule, entity, indicatorFilter, parameter, channel) =>
                {
                    entityRule.Entity = entity;

                    if (channel != null)
                    {
                        channels.Add(channel);
                        entityRule.Channels = channels;
                        channels = new List<ChannelModel>();
                    }

                    if (indicatorFilter != null)
                        entityRule.IndicatorFilter = indicatorFilter;

                    if (parameter != null)
                        entityRule.Parameter = parameter;

                    return entityRule;
                },
                 new { ruleId },
                _connector.dbTransaction,
                splitOn: "Id, Id, Id, Id");
        }

        public async Task<IEnumerable<EntityRuleModel>> GetByEntityAsync(int entityId)
        {
            var channels = new List<ChannelModel>();
            return await _connector.dbConnection.QueryAsync<EntityRuleModel, IndicatorFilterModel, ParameterModel, RuleModel, ChannelModel, EntityRuleModel>($"Select re.Id, re.AlertaId as RuleId, re.EntidadeId as EntityId, re.NmSubEntidade as SubEntityNumber, re.UsuarioIdResponsavel as UserId, re.FiltroId as IndicatorFilterId, re.ParametroId as ParameterId, re.Ativo as Active, " +
                "fi.Id, fi.Descricao as Description, fi.Comando as Command, fi.Ativo as Active, " +
                "pa.Id, pa.Ativo as Active, pa.CriticidadeBaixa as LowSeverity, pa.CriticidadeMedia as MediumSeverity, pa.CriticidadeAlta as HighSeverity, pa.PeriodoIndicador as EvaluationPeriod, pa.PeriodoHistorico as ComparativePeriod, " +
                "ra.Id, ra.Nome as Name, ra.Observacao as Description, ra.TipoId as AlgorithmId, ra.IndicadorId as IndicatorId, ra.OperadorId as OperatorId, ra.FiltroId as IndicatorFilterId, ra.ParametroId as ParameterId, ra.Ativo as Active, ra.DataUltimaAlteracao as LastUpdateDate, ra.UsuarioIdCriacao as UserId, ra.VersaoMajor as VersionMajor, ra.VersaoMinor as VersionMinor, ra.VersaoPatch as VersionPatch, " +
                "c.Id, c.Nome as Name, c.Ativo as Active, c.CanalIdOriginal as OriginalChannelId, c.TipoSubGrupoId as SubgroupId " +
                "from Alertas.RegraEntidade re " +
                "inner join Alertas.RegraAlerta ra on re.AlertaId = ra.Id " +
                "left join Alertas.FiltroIndicador fi on fi.Id = re.FiltroId " +
                "left join Alertas.Parametro pa on pa.Id = re.ParametroId " +
                "left join Alertas.R_RegraEntidadeCanal R_ec on R_ec.RegraEntidadeId = re.Id " +
                "left join Org.Canal c on c.Id = R_ec.CanalId " +
                "where re.EntidadeId = @entityId",
                map: (entityRule, indicatorFilter, parameter, rule, channel) =>
                {
                    entityRule.Rule = rule;

                    if (channel != null)
                    {
                        channels.Add(channel);
                        entityRule.Channels = channels;
                        channels = new List<ChannelModel>();
                    }

                    if (indicatorFilter != null)
                        entityRule.IndicatorFilter = indicatorFilter;
                    if (parameter != null)
                        entityRule.Parameter = parameter;

                    return entityRule;
                },
                 new { entityId },
                _connector.dbTransaction,
                splitOn: "Id, Id, Id");
        }
        public async Task<IEnumerable<Tuple<EntityRuleGroupByRuleDto, int>>> GroupByRuleAsync(int ruleId, EntityRuleQueryParams queryParams)
        {
            return await _connector.dbConnection.QueryAsync<EntityRuleGroupByRuleDto, int, Tuple<EntityRuleGroupByRuleDto, int>>(
               "Select re.Id, CONCAT(e.Nome, CASE WHEN c.Nome = null THEN '' ELSE '/' + c.Nome END) as EntityName, Count(ag.RegraAlertaEntidadeId) as AlertsQuantity, tratativas.Id as TreatmentsQuantity, " +
               "CASE WHEN tratativas.Id = 0 THEN 0 ELSE (convert(float,tratativas.Id) / convert(float,Count(ag.RegraAlertaEntidadeId)) * 100.0) END AS PercentageTreatment, e.Id as EntityId, re.UsuarioIdResponsavel as UserId, re.Ativo as Active, " +
               "(count (*) over (partition by 1)) AS Total from Alertas.RegraEntidade re " +
               "inner join Org.Entidade e on e.Id = re.EntidadeId " +
               "inner join Alertas.RegraAlerta ra on ra.Id = re.AlertaId " +
               "left join Alertas.AlertaGerado ag on ag.RegraAlertaEntidadeId = re.Id " +
               "left join Alertas.R_RegraEntidadeCanal rer on rer.RegraEntidadeId = re.Id " +
               "left join Org.Canal c on rer.CanalId = c.Id " +
               "outer apply( " +
                "select Count(Distinct alerta_tratativa.Data) as Id from Alertas.AlertaGeradoTratativa alerta_tratativa " +
                    "inner join Alertas.R_AlertaGeradoTratativa r_at on r_at.AlertaGeradoTratativaId = alerta_tratativa.Id " +
                    "inner join Alertas.AlertaGerado alerta_gerado on alerta_gerado.Id = r_at.AlertaGeradoId " +
                    "inner join Alertas.RegraEntidade regra_entidade on regra_entidade.Id = alerta_gerado.RegraAlertaEntidadeId " +
                "where regra_entidade.Id = re.Id " +
               ") as tratativas " +
               "where re.AlertaId = @ruleId " +
               $"{(!string.IsNullOrEmpty(queryParams.Search) ? "AND e.Nome like @search " : "")} " +
               "group by re.Id, re.UsuarioIdResponsavel, e.Nome, e.Id, tratativas.Id, re.Ativo, c.Nome " +
               $"ORDER BY {(int)queryParams.OrderCollumn + 1} {queryParams.SortOrders} " +
               "OFFSET @perPage * (@page - 1) ROWS FETCH NEXT @perPage ROWS ONLY",
                map: (entityRule, tuple) =>
                {
                    return new(entityRule, tuple);
                },
                param:
                    new
                    {
                        ruleId,
                        perPage = queryParams.TotalPage,
                        queryParams.Page,
                        search = ("%" + queryParams.Search?.ToLower() + "%")
                    },
                _connector.dbTransaction,
                splitOn: "Total");
        }

        public async Task<IEnumerable<Tuple<EntityRuleGroupByEntityDto, int>>> GroupByEntityAsync(int entityId, EntityRuleQueryParams queryParams)
        {
            return await _connector.dbConnection.QueryAsync<EntityRuleGroupByEntityDto, int, Tuple<EntityRuleGroupByEntityDto, int>>(
               "Select re.Id, ra.Nome as RuleName, Count(ag.RegraAlertaEntidadeId) as AlertsQuantity, tratativas.Id as TreatmentsQuantity, " +
               "CASE WHEN tratativas.Id = 0 THEN 0 ELSE (convert(float,tratativas.Id) / convert(float,Count(ag.RegraAlertaEntidadeId)) * 100.0) END AS PercentageTreatment, ra.Id as RuleId, re.UsuarioIdResponsavel as UserId, re.Ativo as Active, " +
               "(count (*) over (partition by 1)) AS Total from Alertas.RegraEntidade re " +
               "inner join Alertas.RegraAlerta ra on re.AlertaId = ra.Id " +
               "left join Alertas.AlertaGerado ag on ag.RegraAlertaEntidadeId = re.Id " +
               "left join Alertas.R_RegraEntidadeCanal rer on rer.RegraEntidadeId = re.Id " +
               "left join Org.Canal c on rer.CanalId = c.Id " +
               "outer apply( " +
                "select Count(Distinct alerta_tratativa.Data) as Id from Alertas.AlertaGeradoTratativa alerta_tratativa " +
                    "inner join Alertas.R_AlertaGeradoTratativa r_at on r_at.AlertaGeradoTratativaId = alerta_tratativa.Id " +
                    "inner join Alertas.AlertaGerado alerta_gerado on alerta_gerado.Id = r_at.AlertaGeradoId " +
                    "inner join Alertas.RegraEntidade regra_entidade on regra_entidade.Id = alerta_gerado.RegraAlertaEntidadeId " +
                "where regra_entidade.Id = re.Id " +
               ") as tratativas " +
                "where re.EntidadeId = @entityId " +
                $"{(queryParams.ChannelId.HasValue ? "and c.Id = @channelId " : "")} " +
                $"{(!string.IsNullOrEmpty(queryParams.Search) ? "AND ra.Nome like @search " : "")} " +
                "group by re.Id, re.UsuarioIdResponsavel, ra.Nome, ra.Id, tratativas.Id, re.Ativo " +
                $"ORDER BY {(int)queryParams.OrderCollumn + 1} {queryParams.SortOrders} " +
                "OFFSET @perPage * (@page - 1) ROWS FETCH NEXT @perPage ROWS ONLY",
                map: (entityRule, tuple) =>
                {
                    return new(entityRule, tuple);
                },
                param:
                    new
                    {
                        entityId,
                        perPage = queryParams.TotalPage,
                        queryParams.Page,
                        search = ("%" + queryParams.Search?.ToLower() + "%"),
                        channelId = queryParams.ChannelId,
                    },
                _connector.dbTransaction,
                splitOn: "Total");
        }
    }
}
