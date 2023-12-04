using Viabilidade.Domain.Models.Base;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Models.Alert
{
    public class AlertModel: BaseModel
    {
        public int? EntityId { get; set; }
        public string EntityName { get; set; }
        public EntityModel Entity { get; set; }
        public int EntityRuleId { get; set; }
        public virtual EntityRuleModel EntityRule { get; set; }
        public string RuleName { get; set; }
        public DateTime? Version { get; set; }
        public DateTime? IndicatorFirstDate { get; set; }
        public DateTime? IndicatorLastDate { get; set; }
        public string Severity { get; set; }
        public decimal? Indicator { get; set; }
        public decimal? LowReferenceIndicator { get; set; }
        public decimal? MediumReferenceIndicator { get; set; }
        public decimal? HighReferenceIndicator { get; set; }
        public int? StatusId { get; set; }
        public virtual StatusModel Status { get; set; }
        public DateTime? FinishDate { get; set; }
        public Guid? UserId { get; set; }
        public bool? Active { get; set; }
        public bool? Treated { get; set; }
        public decimal? IndicatorValue { get; set; }
        public decimal? PercentageIndicator { get; set; }

        public virtual IEnumerable<AlertChannelModel> AlertChannels { get; set; }
        public virtual IEnumerable<SilencedAlertModel> SilencedAlerts { get; set; }
        public virtual IEnumerable<TreatmentModel> Treatments { get; set; }
    }
}
