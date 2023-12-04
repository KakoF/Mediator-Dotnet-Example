using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Viabilidade.Application.Commands.Alert.Algorithm.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<AlgorithmModel>, AlgorithmModel>
    {
        private readonly IAlgorithmService _algoritmoTipoService;
        
        public GetQueryHandler(IAlgorithmService algoritmoTipoService)
        {
            _algoritmoTipoService = algoritmoTipoService;
        }
        public async Task<AlgorithmModel> Handle(GetRequest<AlgorithmModel> request, CancellationToken cancellationToken)
        {
            return await _algoritmoTipoService.GetAsync(request.Id);
            
        }
    }
}