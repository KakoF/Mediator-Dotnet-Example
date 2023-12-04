using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class SilencedAlertModel :BaseModel   
    {
        public int AlertId { get; set; }
        public virtual AlertModel Alert { get; set; }
        public DateTime StartDateSilence { get; set; }
        public DateTime EndDateSilence { get; set; }
        public bool Active { get; set; }
    }
}
