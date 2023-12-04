using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class RTreatmentAlertModel : BaseModel
    {
        public int? AlertId { get; set; }
        public AlertModel Alert { get; set; }
        public int? TreatmentId { get; set; }
        public TreatmentModel Treatment { get; set; }
    }
}
