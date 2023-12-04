using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Viabilidade.API.Helpers.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var jsonModelValidate = context.ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(new ErrorResponse((int)HttpStatusCode.BadRequest, "Erro em processar os dados de entrada da requisição", jsonModelValidate));
                return;
            }
            await next();
        }
    }
}