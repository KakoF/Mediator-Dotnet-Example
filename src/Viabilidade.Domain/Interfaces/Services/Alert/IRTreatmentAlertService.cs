using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IRTreatmentAlertService
    {
        Task<RTreatmentAlertModel> CreateAsync(RTreatmentAlertModel model);
    }
}
