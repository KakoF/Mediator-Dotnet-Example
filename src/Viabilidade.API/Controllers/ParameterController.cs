using MediatR;
using Microsoft.AspNetCore.Mvc;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Models.Alert;
using Microsoft.AspNetCore.Authorization;
using Viabilidade.API.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class ParameterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParameterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Parâmetros")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ParameterModel>))]
        public async Task<IEnumerable<ParameterModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<ParameterModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Parâmetro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParameterModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ParameterModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<ParameterModel>(id));
        }
    }
}