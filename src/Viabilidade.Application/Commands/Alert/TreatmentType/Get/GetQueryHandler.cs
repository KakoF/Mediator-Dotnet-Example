using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.TreatmentType.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<TreatmentTypeModel>, TreatmentTypeModel>
    {
        private readonly ITreatmentTypeService _tipoTratativaService;
        
        public GetQueryHandler(ITreatmentTypeService tipoTratativaService)
        {
            _tipoTratativaService = tipoTratativaService;
        }
        public async Task<TreatmentTypeModel> Handle(GetRequest<TreatmentTypeModel> request, CancellationToken cancellationToken)
        {
            return await _tipoTratativaService.GetAsync(request.Id);
            
        }
    }
}