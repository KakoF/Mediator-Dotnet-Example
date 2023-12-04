using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.IndicatorFilter.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<IndicatorFilterModel>, IndicatorFilterModel>
    {
        private readonly IIndicatorFilterService _filtroIndicadorService;
        
        public GetQueryHandler(IIndicatorFilterService filtroIndicadorService)
        {
            _filtroIndicadorService = filtroIndicadorService;
        }
        public async Task<IndicatorFilterModel> Handle(GetRequest<IndicatorFilterModel> request, CancellationToken cancellationToken)
        {
            return await _filtroIndicadorService.GetAsync(request.Id);
            
        }
    }
}