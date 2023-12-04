using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class RTreatmentAlertService : IRTreatmentAlertService
    {
        private readonly IRTreatmentAlertRepository _rAlertaGeradoTratativaRepository;
        public RTreatmentAlertService(IRTreatmentAlertRepository rAlertaGeradoTratativaRepository)
        {
            _rAlertaGeradoTratativaRepository = rAlertaGeradoTratativaRepository;
        }
        public async Task<RTreatmentAlertModel> CreateAsync(RTreatmentAlertModel model)
        {
            return await _rAlertaGeradoTratativaRepository.CreateAsync(model);
        }
    }
}
