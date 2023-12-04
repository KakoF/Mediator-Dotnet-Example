using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Viabilidade.API.Helpers.Bindings.CommaSeparatedBiding
{
    public class CommaSeparatedQueryStringConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            foreach (var parameter in action.Parameters)
            {
                if (parameter.Attributes.OfType<CommaSeparatedAttribute>().Any() && !parameter.Action.Filters.OfType<SeparatedQueryStringAttribute>().Any())
                    parameter.Action.Filters.Add(new SeparatedQueryStringAttribute(parameter.ParameterName, ","));
            }
        }
    }
}
