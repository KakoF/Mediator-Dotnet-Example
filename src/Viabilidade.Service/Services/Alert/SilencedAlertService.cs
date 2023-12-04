using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class SilencedAlertService : ISilencedAlertService
    {
        private readonly ISilencedAlertRepository _alertaGeradoSilenciadoRepository;
        public SilencedAlertService(ISilencedAlertRepository alertaGeradoSilenciadoRepository)
        {
            _alertaGeradoSilenciadoRepository = alertaGeradoSilenciadoRepository;
        }
        public async Task<SilencedAlertModel> CreateAsync(SilencedAlertModel model)
        {
            return await _alertaGeradoSilenciadoRepository.CreateAsync(model);
        }

    }
}
