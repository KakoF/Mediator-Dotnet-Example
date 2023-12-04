using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class FavoriteAlertModel : BaseModel
    {
        public Guid? UserId { get; set; }
        public int? RuleId { get; set; }
        public virtual RuleModel Rule { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Active { get; set; }
    }
}
