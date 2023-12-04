using Viabilidade.Domain.Models.Base;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Models.Alert
{
    public class AlertChannelModel : BaseModel
    {
        public int AlertId { get; set; }
        public virtual AlertModel Alert { get; set; }
        public int ChannelId { get; set; }
        public virtual ChannelModel Channel { get; set; }
        public string ChannelName { get; set; }
    }
}