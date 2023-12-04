using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Org
{
    public class RSegmentEntityModel : BaseModel
    {
        public int SegmentId { get; set; }
        public int EntityId { get; set; }
    }
}