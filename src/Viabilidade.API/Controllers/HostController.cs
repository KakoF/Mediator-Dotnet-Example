using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Host.GetUsers;
using Viabilidade.Domain.Models.Client.Host;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class HostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Users")]
        [SwaggerOperation(Summary = "Recupera Lista de Usuários do Atlas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserListDetailModel))]
        public async Task<UserListDetailModel> GetUsersAsync()
        {
            return await _mediator.Send(new GetAllRequest());
        }
    }
}