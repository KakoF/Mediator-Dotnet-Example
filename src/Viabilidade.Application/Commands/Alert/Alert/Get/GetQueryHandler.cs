using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<AlertModel>, AlertModel>
    {
        private readonly IAlertService _alertaGeradoService;
        
        public GetQueryHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        public async Task<AlertModel> Handle(GetRequest<AlertModel> request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoService.GetAsync(request.Id);
        }
    }
}