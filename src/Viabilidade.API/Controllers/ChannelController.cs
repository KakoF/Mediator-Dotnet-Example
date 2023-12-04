using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Application.Commands.Org.Channel.GetBySubgroup;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class ChannelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChannelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Canais")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChannelModel>))]
        public async Task<IEnumerable<ChannelModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<ChannelModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Canal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChannelModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ChannelModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<ChannelModel>(id));
        }

        [HttpGet]
        [Route("Subgroup/{subgroupId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Canais, pelo SubGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChannelModel>))]
        public async Task<IEnumerable<ChannelModel>> GetBySubGrupoAsync(int subgroupId)
        {
            return await _mediator.Send(new GetBySubgroupRequest(subgroupId));
        }
    }
}