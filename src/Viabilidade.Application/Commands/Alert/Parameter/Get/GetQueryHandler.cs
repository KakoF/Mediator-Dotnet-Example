using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Parameter.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<ParameterModel>, ParameterModel>
    {
        private readonly IParameterService _parametroService;
        
        public GetQueryHandler(IParameterService parametroService)
        {
            _parametroService = parametroService;
        }
        public async Task<ParameterModel> Handle(GetRequest<ParameterModel> request, CancellationToken cancellationToken)
        {
            return await _parametroService.GetAsync(request.Id);
            
        }
    }
}