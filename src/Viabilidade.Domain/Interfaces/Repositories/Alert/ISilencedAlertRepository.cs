using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface ISilencedAlertRepository
    {
        Task<SilencedAlertModel> CreateAsync(SilencedAlertModel entity);
    }
}
