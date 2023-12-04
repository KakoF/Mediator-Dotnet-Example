using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class RuleActiveModel : BaseModel   
    {
        public int RuleId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UserId { get; set; }
    }
}
