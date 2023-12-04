using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Segment.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<SegmentModel>, SegmentModel>
    {
        private readonly ISegmentService _segmentoService;
        
        public GetQueryHandler(ISegmentService segmentoService)
        {
            _segmentoService = segmentoService;
        }
        public async Task<SegmentModel> Handle(GetRequest<SegmentModel> request, CancellationToken cancellationToken)
        {
            return await _segmentoService.GetAsync(request.Id);
            
        }
    }
}