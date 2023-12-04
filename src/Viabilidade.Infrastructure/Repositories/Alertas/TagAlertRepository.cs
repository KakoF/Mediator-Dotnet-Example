using Dapper;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class TagAlertRepository : BaseRepository<TagAlertModel>, ITagAlertRepository
    {
        private readonly IDbConnector _connector;
        protected override string _database => "Alertas.AlertaTag";

        protected override string _selectCollumns => "Id, RegraAlertaId as RuleId, TagId as TagId, Ativo as Active";

        public TagAlertRepository(IDbConnector connector) : base(connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<TagAlertModel>> GetByRuleAsync(int ruleId, bool? active = null)
        {
            return await _connector.dbConnection.QueryAsync<TagAlertModel, TagModel, TagAlertModel>($"Select alt.Id, alt.RegraAlertaId as RuleId, alt.TagId, alt.Ativo as Active, " +
                "t.Id, t.Nome as Name, t.IdOriginal as OriginalId, t.Ativo as Active " +
                $"from Alertas.AlertaTag alt " +
                $"inner join Alertas.Tag t on t.Id = alt.TagId " +
                $"where alt.RegraAlertaId = @ruleId {(active != null ? "and ativo = @active" : "")}",
                map: (tagAlert, tag) =>
                {
                    if (tag == null)
                    {
                        return tagAlert;
                    }
                    tagAlert.Tag = tag;
                    return tagAlert;
                },
                new { ruleId, active },
                _connector.dbTransaction,
                splitOn: "Id");
        }
        public async Task<IEnumerable<TagAlertModel>> GetByTagAsync(int tagId, bool? active = null)
        {
            return await _connector.dbConnection.QueryAsync<TagAlertModel>($"Select {_selectCollumns} from {_database} where TagId = @tagId {(active != null ? "and ativo = @active" : "")}", new { tagId, active }, _connector.dbTransaction);
        }

        public async Task<TagAlertModel> CreateAsync(TagAlertModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.AlertaTag" +
                "(RegraAlertaId, TagId, Ativo) OUTPUT Inserted.Id " +
                "VALUES" +
                "(@RegraAlertaId, @TagId, @Ativo) "
                , new { RegraAlertaId = entity.RuleId, entity.TagId, Ativo = entity.Active }, _connector.dbTransaction);
            return entity;
        }

        public async Task<bool> DeleteByRuleAsync(int ruleId)
        {
            int delete = await _connector.dbConnection.ExecuteAsync($"Delete from {_database} where RegraAlertaId = @ruleId ", new { ruleId }, _connector.dbTransaction);
            return Convert.ToBoolean(delete);
        }
    }
}
