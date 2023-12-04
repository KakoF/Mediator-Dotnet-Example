using MediatR;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Entity.GetAllFilter
{
    public class GetAllFilterRequest : IRequest<IEnumerable<EntityModel>>
    {
        public int? Id { get; private set; }
        public string EntidadeIdOriginal { get; private set; }
        public string Nome { get; private set; }

        public GetAllFilterRequest(int? id, string nome, string entidadeIdOriginal)
        {
            Id = id;
            Nome = nome;
            EntidadeIdOriginal = entidadeIdOriginal;
        }

    }
}