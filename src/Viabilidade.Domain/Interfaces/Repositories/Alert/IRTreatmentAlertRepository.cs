using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IRTreatmentAlertRepository
    {
        Task<RTreatmentAlertModel> CreateAsync(RTreatmentAlertModel entity);
    }
}
