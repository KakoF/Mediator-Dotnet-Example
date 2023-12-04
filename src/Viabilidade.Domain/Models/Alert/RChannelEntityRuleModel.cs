using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class RChannelEntityRuleModel : BaseModel
    {
        public int? EntityRuleId { get; set; }
        public int? ChannelId { get; set; }
    }
}
