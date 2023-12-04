using Dapper;
using Viabilidade.Domain.Interfaces.Repositories.Org.Views;
using Viabilidade.Domain.Models.Views;
using Viabilidade.Infrastructure.Interfaces.DataConnector;
using static Dapper.SqlMapper;

namespace Viabilidade.Infrastructure.Repositories.Org.Views
{
    public class VwChannelSquadEntityRepository : IVwChannelSquadEntityRepository
    {

        private readonly IDbConnector _connector;
        public VwChannelSquadEntityRepository(IDbConnector connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<BondModel>> GetAsync(string search, int segmentId)
        {

            var vinculos = await _connector.dbConnection.QueryAsync<BondModel>(@" SELECT 	
                                            vw.SquadId,
                                            vw.SquadNome as SquadName,
	                                        NULL as EntityId,
                                            NULL as EntityName,
	                                        NULL as ChannelId,
                                            NULL as ChannelName,
                                            1 AS TypeId,
	                                        'Squad' AS TypeDescription
                                        FROM Org.VwCanalSquadEntidade vw
                                        WHERE (vw.SquadNome LIKE @query) 
                                        AND (vw.EntidadeSegmentoId = @segmentId)
                                        GROUP BY vw.SquadId , vw.SquadNome

                                        UNION ALL

                                        SELECT 	
	                                        vw.SquadId,
                                            vw.SquadNome as SquadName,
                                            vw.EntidadeId as EntityId,
                                            vw.EntidadeNome as EntityName,
	                                        NULL as ChannelId,
                                            NULL as ChannelName,
                                            2 as TypeId,
	                                        'Entidade' AS TypeDescription
                                        FROM Org.VwCanalSquadEntidade vw
                                        WHERE ((vw.EntidadeNome LIKE @query) OR (vw.EntidadeIdOriginal LIKE @query))
                                        AND (vw.EntidadeSegmentoId = @segmentId)
                                        GROUP BY vw.SquadId , vw.SquadNome, vw.EntidadeId , vw.EntidadeNome

                                        UNION ALL

                                        SELECT 	
	                                        vw.SquadId,
                                            vw.SquadNome as SquadName,
                                            vw.EntidadeId as EntityId,
                                            vw.EntidadeNome as EntityName,
                                            vw.CanalId as ChannelId,
                                            vw.CanalNome as ChannelName,
                                            3 AS TypeId,
	                                        'Canal' AS TypeDescription
                                        FROM Org.VwCanalSquadEntidade vw
                                        WHERE (vw.CanalNome LIKE @query)  
                                        AND (vw.EntidadeSegmentoId = @segmentId)
                                        GROUP BY vw.SquadId , vw.SquadNome, vw.EntidadeId , vw.EntidadeNome, vw.CanalId , vw.CanalNome "
            , new { query = $"%{search}%", segmentId }, _connector.dbTransaction);
            return vinculos;
        }
    }
}
