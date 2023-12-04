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
    public class OperatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Operadores")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperatorModel>))]
        public async Task<IEnumerable<OperatorModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<OperatorModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Operador")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperatorModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<OperatorModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<OperatorModel>(id));
        }
    }
}