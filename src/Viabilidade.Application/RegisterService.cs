using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Viabilidade.Application.Commands.Alert.Alert.UpdateResponsible;
using Viabilidade.Application.Commands.Alert.Alert.UpdateResponsible.Validators;
using Viabilidade.Application.Commands.Alert.Treatment.Create;
using Viabilidade.Application.Commands.Alert.Rule.Create;
using Viabilidade.Application.Commands.Alert.Rule.Create.Validators;
using Viabilidade.Application.Commands.Alert.Rule.Update;
using Viabilidade.Application.Commands.Alert.Rule.Update.Validators;

namespace Viabilidade.Application
{
    public static class RegisterService
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<IValidator<CreateRuleRequest>, CreateRuleValidator>();
            services.AddTransient<IValidator<UpdateRuleRequest>, UpdateRuleValidator>();
            services.AddTransient<IValidator<UpdateResponsibleRequest>, UpdateResponsibleValidator>();
            services.AddTransient<IValidator<CreateTreatmentRequest>, CreateTreatmentValidator>();
        }
    }
}