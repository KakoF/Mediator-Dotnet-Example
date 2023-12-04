using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Viabilidade.Application.Commands.Alert.Algorithm.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<AlgorithmModel>, IEnumerable<AlgorithmModel>>
    {
        private readonly IAlgorithmService _algoritmoTipoService;
        
        public GetAllQueryHandler(IAlgorithmService algoritmoTipoService)
        {
            _algoritmoTipoService = algoritmoTipoService;
        }
        public async Task<IEnumerable<AlgorithmModel>> Handle(GetActivesRequest<AlgorithmModel> request, CancellationToken cancellationToken)
        {
            return await _algoritmoTipoService.GetAsync(request.Active);
            
        }
    }
}