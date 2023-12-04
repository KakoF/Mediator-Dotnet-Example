using System.Xml.Linq;

namespace Viabilidade.Domain.DTO.Treatment
{
    public class TreatmentGroupDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string ChannelName { get; set; }
        public string SquadName { get; set; }
        public string AlertName { get; set; }
        public int TreatmentsQuantity { get; set; }
        public string LastClassName { get; set; }
        private decimal _percentageProblem;
        public decimal PercentageProblem
        {
            get
            {
                return _percentageProblem;
            }
            set
            {
                _percentageProblem = Decimal.Round(value, 2);
            }
        }
        public DateTime Date { get; set; }
        public int AlertId { get; set; }
        public int EntityRuleId { get; set; }
    }
}
