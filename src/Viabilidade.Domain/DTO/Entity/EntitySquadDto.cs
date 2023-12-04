
namespace Viabilidade.Domain.DTO.Entity
{
    public class EntitySquadDto
    {
        public int SquadId { get; set; }
        public string SquadName { get; set; }
        public int EntityId { get; set; }
        public int OriginalEntityId { get; set; }
        public string EntityName { get; set; }
        public int SegmentId { get; set; }
    }
}
