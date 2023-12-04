using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllRequest, IEnumerable<AlertModel>>
    {
        private readonly IAlertService _alertaGeradoService;
        public GetAllQueryHandler(IAlertService alertaGeradoService)
        {
            _alertaGeradoService = alertaGeradoService;
        }
        public async Task<IEnumerable<AlertModel>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoService.GetAsync();

        }
    }
}