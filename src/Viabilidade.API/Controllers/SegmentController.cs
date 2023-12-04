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
    public class SegmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SegmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Segmentos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SegmentModel>))]
        public async Task<IEnumerable<SegmentModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<SegmentModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Segmento")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SegmentModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<SegmentModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<SegmentModel>(id));
        }
    }
}