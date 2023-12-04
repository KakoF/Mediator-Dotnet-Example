using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Operator.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<OperatorModel>, IEnumerable<OperatorModel>>
    {
        private readonly IOperatorService _operadorService;
        
        public GetAllQueryHandler(IOperatorService operadorService)
        {
            _operadorService = operadorService;
        }
        public async Task<IEnumerable<OperatorModel>> Handle(GetActivesRequest<OperatorModel> request, CancellationToken cancellationToken)
        {
            return await _operadorService.GetAsync(request.Active);
            
        }
    }
}