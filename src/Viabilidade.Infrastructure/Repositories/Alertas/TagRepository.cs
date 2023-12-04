using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class TagRepository : BaseRepository<TagModel>, ITagRepository
    {
        protected override string _database => "Alertas.Tag";
        protected override string _selectCollumns => "Id, Nome as Name, IdOriginal as OriginalId, Ativo as Active";

        public TagRepository(IDbConnector connector) : base(connector)
        {
        }

    }
}
