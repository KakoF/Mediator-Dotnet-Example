using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Views
{
    public class BondModel: BaseModel
    {
        public int SquadId { get; set; }
        public string SquadName { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public int TypeId { get; set; }
        public string TypeDescription { get; set; }
    }
}
