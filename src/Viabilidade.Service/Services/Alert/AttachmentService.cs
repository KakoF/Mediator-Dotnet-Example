using Viabilidade.Domain.Exceptions;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _anexoTratativaRepository;
        public AttachmentService(IAttachmentRepository anexoTratativaRepository)
        {
            _anexoTratativaRepository = anexoTratativaRepository;
        }
        public async Task<AttachmentModel> CreateAsync(AttachmentModel model)
        {
            return await _anexoTratativaRepository.CreateAsync(model);
        }
      
        public async Task<AttachmentModel> GetAsync(int id)
        {
            var model = await _anexoTratativaRepository.GetAsync(id);
            if(model == null)
                throw new DomainException("Arquivo não encontrado", 404);
            return model;
        }

        public async Task<IEnumerable<AttachmentModel>> GetByTreatmentAsync(int treatmentId)
        {
            return await _anexoTratativaRepository.GetByTreatmentAsync(treatmentId);
        }
    }
}
