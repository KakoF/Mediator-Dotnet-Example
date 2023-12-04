using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Indicator.GetBySegment
{
    public class GetBySegmentQueryHandler : IRequestHandler<GetBySegmentRequest, IEnumerable<IndicatorModel>>
    {
        private readonly IIndicatorService _indicadorService;
        
        public GetBySegmentQueryHandler(IIndicatorService indicadorService)
        {
            _indicadorService = indicadorService;
        }
        public async Task<IEnumerable<IndicatorModel>> Handle(GetBySegmentRequest request, CancellationToken cancellationToken)
        {
            return await _indicadorService.GetBySegmentAsync(request.SegmentId, true);
            
        }
    }
}