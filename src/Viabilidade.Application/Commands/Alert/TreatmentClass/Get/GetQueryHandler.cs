using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.TreatmentClass.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<TreatmentClassModel>, TreatmentClassModel>
    {
        private readonly ITreatmentClassService _classeTratativaService;
        
        public GetQueryHandler(ITreatmentClassService classeTratativaService)
        {
            _classeTratativaService = classeTratativaService;
        }
        public async Task<TreatmentClassModel> Handle(GetRequest<TreatmentClassModel> request, CancellationToken cancellationToken)
        {
            return await _classeTratativaService.GetAsync(request.Id);
            
        }
    }
}