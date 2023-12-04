using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Application.Commands.Org.Entity.GetAllFilter;
using Viabilidade.Application.Commands.Org.Entity.GetBySegmentSquad;
using Viabilidade.Application.Commands.Org.Entity.GetBySquads;
using Viabilidade.Application.Commands.Org.Entity.Validate;
using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class EntityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera a Entidade")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EntityModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<EntityModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<EntityModel>(id));
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Entidades")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntityModel>))]
        public async Task<IEnumerable<EntityModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<EntityModel>(active));
        }

        [HttpGet]
        [Route("Filter")]
        [SwaggerOperation(Summary = "Recupera Lista de Entidades (Filtros)")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntityModel>))]
        public async Task<IEnumerable<EntityModel>> GetAllAsync([FromQuery] int? id, [FromQuery] string name, [FromQuery] string entityId)
        {
            return await _mediator.Send(new GetAllFilterRequest(id, name, entityId));
        }

        [HttpGet]
        [Route("Squad/{squadId}/Segment/{segmentId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Entidades, pela Squad e Segmento ")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntityModel>))]
        public async Task<IEnumerable<EntityModel>> GetBySegmentoSquadAsync(int squadId, int segmentId)
        {
            return await _mediator.Send(new GetBySegmentSquadRequest(squadId, segmentId));
        }

        [HttpGet]
        [Route("Squad")]
        [SwaggerOperation(Summary = "Recupera Lista de Entidades pela Squad")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntitySquadDto>))]
        public async Task<IEnumerable<EntitySquadDto>> GetBySquadAsync([FromQuery] IEnumerable<int> squadIds)
        {
            return await _mediator.Send(new GetBySquadsRequest(squadIds));
        }

        [HttpPost]
        [Route("Validate")]
        [SwaggerOperation(Summary = "Valida se as entidades existem e pertecem ao segmento informado")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EntityModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<EntityValidateDto> ValidateAsync([FromBody] ValidateRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}