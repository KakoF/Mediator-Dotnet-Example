using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class TagAlertModel : BaseModel
    {
        public int RuleId { get; set; }
        public virtual RuleModel Rule { get; set; }
        public int TagId { get; set; }
        public virtual TagModel Tag { get; set; }
        public bool Active { get; set; }

        public TagAlertModel()
        {
        }

        public TagAlertModel(int tagId, bool active)
        {
            TagId = tagId;
            Active = active;
        }

    }
}
