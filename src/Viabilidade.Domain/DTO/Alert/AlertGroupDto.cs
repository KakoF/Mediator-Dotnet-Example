namespace Viabilidade.Domain.DTO.Alert
{
    public class AlertGroupDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public int EntityRuleId { get; set; }
        public int AlertsQuantity { get; set; }
        public string ChannelName { get; set; }
        public string AlertName { get; set; }
        public decimal Value { get; set; }
        public string Severity { get; set; }
        public DateTime Date { get; set; }
        public string Version { get; set; }
        public bool LastVersion { get; set; }
    }
}
