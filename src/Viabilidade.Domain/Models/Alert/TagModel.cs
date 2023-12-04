using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class TagModel : BaseModel
    {
        public string Name { get; set; }
        public int OriginalId { get; set; }
        public bool Active { get; set; }
        public virtual IEnumerable<TagAlertModel> TagAlerts { get; set; }
    }
}
