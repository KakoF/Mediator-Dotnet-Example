using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Indicator.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<IndicatorModel>, IEnumerable<IndicatorModel>>
    {
        private readonly IIndicatorService _indicadorService;
        
        public GetAllQueryHandler(IIndicatorService indicadorService)
        {
            _indicadorService = indicadorService;
        }
        public async Task<IEnumerable<IndicatorModel>> Handle(GetActivesRequest<IndicatorModel> request, CancellationToken cancellationToken)
        {
            return await _indicadorService.GetAsync(request.Active);
            
        }
    }
}