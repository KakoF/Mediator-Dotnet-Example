using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Treatment.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<TreatmentModel>, TreatmentModel>
    {
        private readonly ITreatmentService _alertaGeradoTratativaService;
        
        public GetQueryHandler(ITreatmentService alertaGeradoTratativaService)
        {
            _alertaGeradoTratativaService = alertaGeradoTratativaService;
        }
        public async Task<TreatmentModel> Handle(GetRequest<TreatmentModel> request, CancellationToken cancellationToken)
        {
            return await _alertaGeradoTratativaService.GetAsync(request.Id);
            
        }
    }
}