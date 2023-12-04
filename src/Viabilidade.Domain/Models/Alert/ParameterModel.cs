using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class ParameterModel: BaseModel
    {
        public bool? Active { get; set; }
        public decimal? LowSeverity { get; set; }
        public decimal? MediumSeverity { get; set; }
        public decimal? HighSeverity { get; set; }
        public decimal? EvaluationPeriod { get; set; }
        public decimal? ComparativePeriod { get; set; }
        public virtual IEnumerable<RuleModel> Rules { get; set; }
        public virtual IEnumerable<EntityRuleModel> EntityRules { get; set; }

        public ParameterModel()
        {
        }

        public ParameterModel(bool active, decimal highSeverity, decimal mediumSeverity, decimal lowSeverity, decimal? comparativePeriod, decimal evaluationPeriod)
        {
            Active = active;
            HighSeverity = highSeverity;
            MediumSeverity = mediumSeverity;
            LowSeverity = lowSeverity;
            ComparativePeriod = comparativePeriod;
            EvaluationPeriod = evaluationPeriod;
        }
    }
}
