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
    public class SubgroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubgroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de SubGrupos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubgroupModel>))]
        public async Task<IEnumerable<SubgroupModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<SubgroupModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o SubGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubgroupModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<SubgroupModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<SubgroupModel>(id));
        }
    }
}