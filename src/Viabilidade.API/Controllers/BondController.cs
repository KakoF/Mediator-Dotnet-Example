using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Org.Bond.Get;
using Viabilidade.Domain.Models.Views;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class BondController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BondController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Vinculos da Entidade")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BondModel>))]
        public async Task<IEnumerable<BondModel>> GetAsync([FromQuery] string search, [FromQuery] int segmentId)
        {
            return await _mediator.Send(new GetBondRequest(search, segmentId));
        }
    }
}