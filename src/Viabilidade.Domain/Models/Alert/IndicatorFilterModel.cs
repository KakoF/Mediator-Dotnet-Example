using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class IndicatorFilterModel : BaseModel
    {
        public string Description { get; set; }
        public string Command { get; set; }
        public bool Active { get; set; }
        public virtual IEnumerable<RuleModel> Rules { get; set; }
        public virtual IEnumerable<EntityRuleModel> EntityRules { get; set; }
    }
}
