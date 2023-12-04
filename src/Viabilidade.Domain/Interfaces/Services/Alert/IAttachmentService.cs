using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IAttachmentService
    {
        Task<AttachmentModel> CreateAsync(AttachmentModel model);
        Task<AttachmentModel> GetAsync(int id);
        Task<IEnumerable<AttachmentModel>> GetByTreatmentAsync(int treatmentId);
    }
}
