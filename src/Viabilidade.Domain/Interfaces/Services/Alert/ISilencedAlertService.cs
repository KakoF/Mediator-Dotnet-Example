using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface ISilencedAlertService
    {
        Task<SilencedAlertModel> CreateAsync(SilencedAlertModel model);
    }
}
