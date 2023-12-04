using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Tags")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TagModel>))]
        public async Task<IEnumerable<TagModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<TagModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera a Tag")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<TagModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<TagModel>(id));
        }
    }
}