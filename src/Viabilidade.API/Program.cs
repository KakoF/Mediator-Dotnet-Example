using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NLog.Web;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Viabilidade.API.Helpers;
using Viabilidade.API.Helpers.Bindings.CommaSeparatedBiding;
using Viabilidade.API.Helpers.Filters;
using Viabilidade.API.Helpers.Middleware;
using Viabilidade.Application;
using Viabilidade.Domain.Interfaces.Client;
using Viabilidade.Domain.Models.Enums;
using Viabilidade.Infrastructure;
using Viabilidade.Infrastructure.Clients;
using Viabilidade.Infrastructure.Environment;
using Viabilidade.Service;

var builder = WebApplication.CreateBuilder(args);

NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = builder.Configuration;
var logger =
    NLogBuilder.ConfigureNLog(AspNetEnvironment.IsProductionOrStaging() ?
    "nlog.config" :
    $"nlog.{AspNetEnvironment.GetEnvironment()}.config")
        .GetCurrentClassLogger();

builder.Services.AddCors(o => o.AddPolicy(name: "allowSpecificOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true)
                            .WithExposedHeaders(
                               "SESSID",
                               "redirectUrl",
                               "Origin",
                               "fileName",
                               "message",
                               "X-XSS-Protection",
                               "X-Permitted-Cross-Domain-Policies",
                               "Referrer-Policy",
                               "Content-Security-Policy",
                               "X-Frame-Options",
                               "X-Content-Type-Options",
                               "Strict-Transport-Security"); // allow any origin
                    }));

builder.Services.AddControllers(options =>
{
    options.ValueProviderFactories.Insert(0, new SeparatedQueryStringValueProviderFactory(","));
    options.Filters.Add(typeof(ValidationFilter));
    options.Filters.Add(typeof(NotificationFilter));

}).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssembly(typeof(Program).Assembly))
  .AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
  .AddJsonOptions(opt =>
  {
      //opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      opt.JsonSerializerOptions.Converters.Add(new OptOutJsonConverterFactory(new JsonStringEnumConverter(), typeof(eFileType)));
  });


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events.OnRedirectToAccessDenied =
        options.Events.OnRedirectToLogin = c =>
        {
            c.Response.StatusCode = StatusCodes.Status403Forbidden;
            var notification = JsonConvert.SerializeObject(new ErrorResponse(403));
            c.Response.WriteAsync(notification);
            return Task.FromResult<object>(null);
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Atlas", policy => policy.RequireClaim(ClaimTypes.Role, "VA:Admin"));
});

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);


/*builder.Host
    .ConfigureAppConfiguration((context, builder) =>
    {
        bool.TryParse(context.Configuration["LocalExecution"], out bool localExecution);
        if (!localExecution)
            ConfigureKeyVaultExtension.Configure(builder, context.Configuration["KeyVault:Url"]);
    });*/

builder.Host.UseNLog();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.CustomSchemaIds(type => type.ToString());
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Atlas.Viabilidade API", Version = "v2" });
    c.AddSecurityDefinition("Sessid", new OpenApiSecurityScheme
    {
        Name = "Sessid",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Id da sessão"
    });
    c.AddSecurityDefinition("User-Agent", new OpenApiSecurityScheme
    {
        Name = "User-Agent",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Agente de solicitação"
    });

    c.OperationFilter<AutenticationHeaders>();
});

builder.Services.ConfigureService(builder.Configuration);
builder.Services.ConfigureInfraStructure(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);

builder.Services.AddHttpClient<IHostClient, HostClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Clients:Host:BasePath"]);
});

var app = builder.Build();

app.UseSwagger(c =>
{
    bool.TryParse(builder.Configuration["LocalExecution"], out bool localExecution);
    if (!localExecution)
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = $"https://{httpReq.Host.Value}/analytics/v2/" }
        });
    }
});
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1);
    c.SwaggerEndpoint("v2/swagger.json", "Atlas.Viabilidade API");
});

app.UseHttpsRedirection();

app.UseCors("allowSpecificOrigins");
app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseMiddleware<MiddlewareAuthentication>();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
