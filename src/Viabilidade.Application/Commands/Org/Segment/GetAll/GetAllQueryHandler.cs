using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Segment.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<SegmentModel>, IEnumerable<SegmentModel>>
    {
        private readonly ISegmentService _segmentoService;
        
        public GetAllQueryHandler(ISegmentService segmentoService)
        {
            _segmentoService = segmentoService;
        }
        public async Task<IEnumerable<SegmentModel>> Handle(GetActivesRequest<SegmentModel> request, CancellationToken cancellationToken)
        {
            return await _segmentoService.GetAsync(request.Active);
            
        }
    }
}