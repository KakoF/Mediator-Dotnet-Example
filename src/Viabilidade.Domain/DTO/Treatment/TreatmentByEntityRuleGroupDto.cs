namespace Viabilidade.Domain.DTO.Treatment
{
    public class TreatmentByEntityRuleGroupDto
    {
        public int Id { get; set; }
        public int TreatmentClassId { get; set; }
        public string TreatmentClass { get; set; }
        public string Description { get; set; }
        public int AlertsQuantity { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int AlertId { get; set; }
    }
}
