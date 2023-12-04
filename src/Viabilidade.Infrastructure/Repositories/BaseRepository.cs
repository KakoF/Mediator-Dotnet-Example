using Dapper;
using Viabilidade.Domain.Interfaces.Repositories;
using Viabilidade.Domain.Models.Base;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly IDbConnector _connector;
        protected abstract string _database { get; }
        protected abstract string _selectCollumns { get; }
        public BaseRepository(IDbConnector connector)
        {
            _connector = connector;
        }

        public async Task<IEnumerable<T>> GetAsync(bool? active = null)
        {
            return await _connector.dbConnection.QueryAsync<T>($"Select {_selectCollumns} from {_database} {(active != null ? "where ativo = @active" : "")}", new { active }, _connector.dbTransaction);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<T>($"Select {_selectCollumns} from {_database} where id = @id", new { id }, _connector.dbTransaction);
        }

    }
}
