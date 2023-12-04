using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Operator.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<OperatorModel>, OperatorModel>
    {
        private readonly IOperatorService _operadorService;
        
        public GetQueryHandler(IOperatorService operadorService)
        {
            _operadorService = operadorService;
        }
        public async Task<OperatorModel> Handle(GetRequest<OperatorModel> request, CancellationToken cancellationToken)
        {
            return await _operadorService.GetAsync(request.Id);
            
        }
    }
}