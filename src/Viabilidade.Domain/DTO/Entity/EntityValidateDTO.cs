
namespace Viabilidade.Domain.DTO.Entity
{
    public class EntityValidateDto
    {
        public IList<EntitySquadDto> Valids { get; set; } = new List<EntitySquadDto>();
        public IList<InvalidEntityDTO> Invalids { get; set; } = new List<InvalidEntityDTO>();
    }

    public class InvalidEntityDTO
    {
        public InvalidEntityDTO(string entityId, string description)
        {
            EntityId = entityId;
            Description = description;
        }

        public string EntityId { get; set; }
        public string Description { get; set; }
    }
}
