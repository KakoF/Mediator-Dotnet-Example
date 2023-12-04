using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.TreatmentType.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<TreatmentTypeModel>, IEnumerable<TreatmentTypeModel>>
    {
        private readonly ITreatmentTypeService _tipoTratativaService;
        
        public GetAllQueryHandler(ITreatmentTypeService tipoTratativaService)
        {
            _tipoTratativaService = tipoTratativaService;
        }
        public async Task<IEnumerable<TreatmentTypeModel>> Handle(GetActivesRequest<TreatmentTypeModel> request, CancellationToken cancellationToken)
        {
            return await _tipoTratativaService.GetAsync(request.Active);
            
        }
    }
}