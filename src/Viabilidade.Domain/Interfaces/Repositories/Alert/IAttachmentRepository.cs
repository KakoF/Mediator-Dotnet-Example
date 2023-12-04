using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface IAttachmentRepository
    {

        Task<AttachmentModel> CreateAsync(AttachmentModel model);
        Task<AttachmentModel> GetAsync(int id);
        Task<IEnumerable<AttachmentModel>> GetByTreatmentAsync(int treatmentId);
    }
}
