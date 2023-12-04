using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Org
{
    public class EntityModel : BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public int OriginalEntityId { get; set; }
        public virtual IEnumerable<SegmentModel> Segments { get; set; }
        public virtual IEnumerable<ChannelModel> Channels { get; set; }
        public virtual IEnumerable<SquadModel> Squads { get; set; }
    }
}