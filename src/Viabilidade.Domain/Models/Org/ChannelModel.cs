using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Org
{
    public class ChannelModel: BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public int OriginalChannelId { get; set; }
        public int SubgroupId { get; set; }
        public virtual SubgroupModel Subgroup { get; set; }

        public ChannelModel()
        {
        }

        public ChannelModel(int channelId)
        {
            Id = channelId;
        }

    }
}
