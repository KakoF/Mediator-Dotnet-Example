using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class AlgorithmModel : BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public virtual IEnumerable<RuleModel> Rules { get; set; }
    }
}
