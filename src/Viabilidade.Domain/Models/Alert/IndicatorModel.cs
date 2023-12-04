using Viabilidade.Domain.Models.Base;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Models.Alert
{
    public class IndicatorModel : BaseModel
    {
        public string Description { get; set; }
        public string Command { get; set; }
        public bool Active { get; set; }
        public string SQLIndicator { get; set; }
        public int SegmentId { get; set; }
        public virtual SegmentModel Segment { get; set; }
        public virtual IEnumerable<RuleModel> Rules { get; set; }
    }
}
