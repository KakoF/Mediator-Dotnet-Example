using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.IndicatorFilter.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<IndicatorFilterModel>, IEnumerable<IndicatorFilterModel>>
    {
        private readonly IIndicatorFilterService _filtroIndicadorService;
        
        public GetAllQueryHandler(IIndicatorFilterService filtroIndicadorService)
        {
            _filtroIndicadorService = filtroIndicadorService;
        }
        public async Task<IEnumerable<IndicatorFilterModel>> Handle(GetActivesRequest<IndicatorFilterModel> request, CancellationToken cancellationToken)
        {
            return await _filtroIndicadorService.GetAsync(request.Active);
            
        }
    }
}