using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class TreatmentClassModel : BaseModel
    {
        public string Name { get; set; }
        public string Concept { get; set; }
        public bool Active { get; set; }

        public virtual IEnumerable<TreatmentModel> Treatments { get; set; }
        public virtual IEnumerable<TreatmentTypeModel> TreatmentTypes { get; set; }
    }
}
