using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class TreatmentTypeModel : BaseModel
    {
        public string Description { get; set; }
        public int TreatmentClassId { get; set; }
        public virtual TreatmentClassModel TreatmentClass { get; set; }
        public virtual IEnumerable<TreatmentModel> Treatments { get; set; }
        public string TreatmentTypeConcept { get; set; }
        public bool Active { get; set; }
    }
}
