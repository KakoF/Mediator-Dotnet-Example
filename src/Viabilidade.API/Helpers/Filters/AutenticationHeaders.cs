using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Viabilidade.API.Helpers.Filters
{
    public class AutenticationHeaders : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Security ??= new List<OpenApiSecurityRequirement>();

            var sessid = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Sessid" } };
            var userAgent = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "User-Agent" } };

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [sessid] = new List<string>(),
                [userAgent] = new List<string>(),
            });
        }
    }
}