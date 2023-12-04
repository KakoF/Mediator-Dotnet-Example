using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Tag.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<TagModel>, TagModel>
    {
        private readonly ITagService _tagService;
        
        public GetQueryHandler(ITagService tagService)
        {
            _tagService = tagService;
        }
        public async Task<TagModel> Handle(GetRequest<TagModel> request, CancellationToken cancellationToken)
        {
            return await _tagService.GetAsync(request.Id);
            
        }
    }
}