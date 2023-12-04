using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.DTO.Rule
{
    public class RuleGroupDto
    {

        public int Id { get; set; }
        public string RuleName { get; set; }
        public DateTime LastChange { get; set; }
        public int? EntitiesQuantity { get; set; }
        public int AlertsQuantity { get; set; }
        public int TreatmentsQuantity { get; set; }
        private decimal _percentageTreatment;
        public decimal PercentageTreatment
        {
            get
            {
                return _percentageTreatment;
            }
            set
            {
                _percentageTreatment = decimal.Round(value, 2);
            }
        }
        public int? RulesQuantity { get; set; }
        public int? EntityId { get; set; }
        public string EntityName { get; set; }
        public int? ChannelId { get; set; }
        public IEnumerable<TagAlertModel> RuleTags { get; set; }

    }
}
