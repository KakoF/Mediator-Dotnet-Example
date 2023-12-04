namespace Viabilidade.Domain.DTO.EntityRule
{
    public class EntityRuleGroupByEntityDto
    {

        public int Id { get; set; }
        public int RuleId { get; set; }
        public string RuleName { get; set; }
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
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool? Active { get; set; }

    }
}
