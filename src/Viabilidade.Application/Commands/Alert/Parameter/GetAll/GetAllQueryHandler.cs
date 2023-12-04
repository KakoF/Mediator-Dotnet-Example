using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Parameter.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<ParameterModel>, IEnumerable<ParameterModel>>
    {
        private readonly IParameterService _parametroService;
        
        public GetAllQueryHandler(IParameterService parametroService)
        {
            _parametroService = parametroService;
        }
        public async Task<IEnumerable<ParameterModel>> Handle(GetActivesRequest<ParameterModel> request, CancellationToken cancellationToken)
        {
            return await _parametroService.GetAsync(request.Active);
            
        }
    }
}