using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.TreatmentClass.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<TreatmentClassModel>, IEnumerable<TreatmentClassModel>>
    {
        private readonly ITreatmentClassService _classeTratativaService;
        
        public GetAllQueryHandler(ITreatmentClassService classeTratativaService)
        {
            _classeTratativaService = classeTratativaService;
        }
        public async Task<IEnumerable<TreatmentClassModel>> Handle(GetActivesRequest<TreatmentClassModel> request, CancellationToken cancellationToken)
        {
            return await _classeTratativaService.GetAsync(request.Active);
            
        }
    }
}