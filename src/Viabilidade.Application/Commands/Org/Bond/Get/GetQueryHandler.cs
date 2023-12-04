using MediatR;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Views;

namespace Viabilidade.Application.Commands.Org.Bond.Get
{
    public class GetQueryHandler : IRequestHandler<GetBondRequest, IEnumerable<BondModel>>
    {
        private readonly IBondService _vinculoService;
        
        public GetQueryHandler(IBondService vinculoService)
        {
            _vinculoService = vinculoService;
        }
        public async Task<IEnumerable<BondModel>> Handle(GetBondRequest request, CancellationToken cancellationToken)
        {
            return await _vinculoService.GetAsync(request.Search, request.SegmentId);
            
        }
    }
}