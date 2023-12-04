using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.DTO.Treatment
{
    public class TreatmentPreviewDto
    {
        public TreatmentModel Treatment { get; set; }
        public RuleModel Rule { get; set; }
        public EntityModel Entity { get; set; }
    }
}
