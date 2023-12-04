using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.UpdateResponsible
{
    public class UpdateResponsibleCommandHandler : IRequestHandler<UpdateResponsibleRequest, AlertModel>
    {
        private readonly IAlertService _alertaGeradoService;
        public UpdateResponsibleCommandHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        public async Task<AlertModel> Handle(UpdateResponsibleRequest request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoService.UpdateUserAsync(request.Id, new AlertModel() { UserId = request.UserId });
        }
    }
}