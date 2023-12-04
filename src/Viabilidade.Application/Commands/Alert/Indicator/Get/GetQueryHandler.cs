using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Indicator.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<IndicatorModel>, IndicatorModel>
    {
        private readonly IIndicatorService _indicadorService;
        
        public GetQueryHandler(IIndicatorService indicadorService)
        {
            _indicadorService = indicadorService;
        }
        public async Task<IndicatorModel> Handle(GetRequest<IndicatorModel> request, CancellationToken cancellationToken)
        {
            return await _indicadorService.GetAsync(request.Id);
            
        }
    }
}