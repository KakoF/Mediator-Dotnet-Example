using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class SquadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SquadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Squads")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SquadModel>))]
        public async Task<IEnumerable<SquadModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<SquadModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Squad")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SquadModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<SquadModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<SquadModel>(id));
        }
    }
}