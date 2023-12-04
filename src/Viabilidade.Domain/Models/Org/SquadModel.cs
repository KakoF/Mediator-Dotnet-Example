using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Org
{
    public class SquadModel : BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}