using Dapper;
using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Models.Org;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Org
{
    public class EntityRepository : BaseRepository<EntityModel>, IEntityRepository
    {
        protected override string _database => "Org.Entidade";
        protected override string _selectCollumns => "Id, Nome as Name, Ativo as Active, EntidadeIdOriginal as OriginalEntityId";
        private readonly IDbConnector _connector;

        public EntityRepository(IDbConnector connector) : base(connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<EntityModel>> GetAllFilter(int? id, string name, string originalEntityId)
        {
            var sql = @$"Select TOP 50 {_selectCollumns} from {_database} Where Ativo = 1
                {(!string.IsNullOrEmpty(name) ? " AND Nome like @Nome" : "")}
                {(id.HasValue && id > 0 ? " AND Id = @id" : "")}
                {(!string.IsNullOrEmpty(originalEntityId) ? " AND CAST(EntidadeIdOriginal AS VARCHAR(MAX)) like @EntidadeIdOriginal " : " ")} 
                ORDER BY Nome";

            return await _connector.dbConnection.QueryAsync<EntityModel>(sql, new { id, Nome = $"%{name}%", EntidadeIdOriginal = $"%{originalEntityId}%" }, _connector.dbTransaction);
        }

        public async Task<IEnumerable<EntityModel>> GetBySquadsAsync(IEnumerable<int> squadIds)
        {
            var sql = @$"SELECT ent.Id, ent.Nome as Name, ent.EntidadeIdOriginal as OriginalEntityId, ent.Ativo as Active, rel.SquadId
                        FROM {_database} ent
                        INNER JOIN Org.R_SquadEntidade rel ON rel.EntidadeId = ent.Id
                        WHERE ent.Ativo = 1 AND rel.SquadId in @squadIds";

            return await _connector.dbConnection.QueryAsync<EntityModel>(sql, new { squadIds });
        }
       
        public async Task<IEnumerable<EntityModel>> GetBySegmentSquadAsync(int squadId, int segmentId)
        {
            return await _connector.dbConnection.QueryAsync<EntityModel>($"SELECT ent.Id, ent.Nome as Name, ent.EntidadeIdOriginal as OriginalEntityId, ent.Ativo as Active " +
                            $"FROM {_database} ent " +
                            "INNER JOIN Org.R_SquadEntidade relsquad ON relsquad.EntidadeId = ent.Id " +
                            "INNER JOIN Org.R_SegmentoEntidade relsegment ON relsegment.EntidadeId = ent.Id " +
                            "WHERE ent.Ativo = 1 AND relsquad.SquadId = @squadId and relsegment.SegmentoId = @segmentId ", new { squadId, segmentId }, _connector.dbTransaction);
        }


        public async Task<IEnumerable<EntitySquadDto>> GetByOriginalIdsAsync(IEnumerable<int> originalEntityIds)
        {
            var sql = @"SELECT 	
	                    S.Id as SquadId,
                        S.Nome as SquadName,
                        E.Id as EntityId,
                        E.EntidadeIdOriginal as OriginalEntityId,
                        E.Nome as EntityName,
                        se.SegmentoId as SegmentId
                    FROM [Org].[Entidade] E
                    left JOIN Org.R_SquadEntidade sqe ON E.Id = sqe.EntidadeId 
					left JOIN Org.R_SegmentoEntidade se ON E.Id = se.EntidadeId 
                    left join [Org].[Squad] S on sqe.SquadId = S.Id
                    WHERE E.EntidadeIdOriginal in @originalEntityIds AND E.Ativo = 1";


            return await _connector.dbConnection.QueryAsync<EntitySquadDto>(sql, new { originalEntityIds }, _connector.dbTransaction);
        }


        public async Task<IEnumerable<EntitySquadDto>> GetBySquadIdsAsync(IEnumerable<int> squadIds)
        {
            var sql = @"SELECT 	
	                    S.Id as SquadId,
                        S.Nome as SquadName,
                        E.Id as EntityId,
                        E.EntidadeIdOriginal as OriginalEntityId,
                        E.Nome as EntityName,
                        E.SegmentoId as SegmentId
                    FROM [Org].[Entidade] E
                    left JOIN Org.R_SquadEntidade sqe ON E.Id = sqe.EntidadeId 
                    left join [Org].[Squad] S on sqe.SquadId = S.Id
                    WHERE S.Id in @squadIds AND E.Ativo = 1";

            return await _connector.dbConnection.QueryAsync<EntitySquadDto>(sql, new { squadIds }, _connector.dbTransaction);
        }



        public async Task<IEnumerable<EntityModel>> GetByOriginalEntityAsync(IEnumerable<int> originalEntityIds)
        {
            return await _connector.dbConnection.QueryAsync<EntityModel>(
                "SELECT ent.Id, ent.Nome as Name, ent.EntidadeIdOriginal as OriginalEntityId, ent.Ativo as Active, " +
                "s.Id, s.Nome As Name, s.Ativo As Active " +
                $"FROM {_database} ent " +
                "left JOIN Org.R_SquadEntidade relsquad ON relsquad.EntidadeId = ent.Id " +
                "left join Org.Squad s on s.Id = relsquad.SquadId " +
                "where ent.EntidadeIdOriginal = @originalEntityIds",
                new { originalEntityIds },
                _connector.dbTransaction
                );
        }

        public async Task<EntityModel> GetByOriginalEntityAsync(int originalEntityId)
        {
            return (await GetByOriginalEntityAsync(new int[1] { originalEntityId })).FirstOrDefault();
        }

        public async Task<EntityModel> GetByIdAsync(int id)
        {
            var squads = new List<SquadModel>();
            return (await _connector.dbConnection.QueryAsync<EntityModel, SquadModel, EntityModel>(
                "SELECT ent.Id, ent.Nome as Name, ent.EntidadeIdOriginal as OriginalEntityId, ent.Ativo as Active, " +
                "s.Id, s.Nome As Name, s.Ativo As Active " +
                $"FROM {_database} ent " +
                "left JOIN Org.R_SquadEntidade relsquad ON relsquad.EntidadeId = ent.Id " +
                "left join Org.Squad s on s.Id = relsquad.SquadId " +
                "where ent.Id = @id",
                map: (entity, squad) =>
                {
                    if (squad != null)
                    {
                        if (squads.FirstOrDefault(x => x.Id == squad.Id) == null)
                            squads.Add(squad);
                    }
                    entity.Squads = squads;
                    return entity;
                },
                param: new { id },
                _connector.dbTransaction,
                splitOn: "Id")).FirstOrDefault();
        }
    }
}
