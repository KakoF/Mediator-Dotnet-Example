using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.TreatmentType.GetByTreatmentClass
{
    public class GetByTreatmentClassQueryHandler : IRequestHandler<GetByTreatmentClassRequest, IEnumerable<TreatmentTypeModel>>
    {
        private readonly ITreatmentTypeService _tipoTratativaService;
        
        public GetByTreatmentClassQueryHandler(ITreatmentTypeService tipoTratativaService)
        {
            _tipoTratativaService = tipoTratativaService;
        }
        public async Task<IEnumerable<TreatmentTypeModel>> Handle(GetByTreatmentClassRequest request, CancellationToken cancellationToken)
        {
            return await _tipoTratativaService.GetByTreatmentClassAsync(request.TreatmentClassId, true);
            
        }
    }
}