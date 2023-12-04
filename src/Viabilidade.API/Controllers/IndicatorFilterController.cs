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
    public class IndicatorFilterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndicatorFilterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Filtros Indicador")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IndicatorFilterModel>))]
        public async Task<IEnumerable<IndicatorFilterModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<IndicatorFilterModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Filtro Indicador")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IndicatorFilterModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IndicatorFilterModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<IndicatorFilterModel>(id));
        }
    }
}