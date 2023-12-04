using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Tag.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<TagModel>, IEnumerable<TagModel>>
    {
        private readonly ITagService _tagService;
        
        public GetAllQueryHandler(ITagService tagService)
        {
            _tagService = tagService;
        }
        public async Task<IEnumerable<TagModel>> Handle(GetActivesRequest<TagModel> request, CancellationToken cancellationToken)
        {
            return await _tagService.GetAsync(request.Active);
            
        }
    }
}