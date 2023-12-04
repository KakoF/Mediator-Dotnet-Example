using MediatR;
using System.Text.Json.Serialization;
using Viabilidade.Application.Commands.Alert.Rule.Create;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.Update
{
    public class UpdateRuleRequest : CreateRuleRequest, IRequest<RuleModel>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public UpdateRuleRequest(int id, string name, string description, int algorithmId, int indicatorId, int operatorId, bool active, bool pinned, CreateParameterRequest parameter, IEnumerable<CreateTagsRequest> tags, IEnumerable<CreateEntityRuleRequest> entityRules)
        {
            Id = id;
            Name = name;
            Description = description;
            AlgorithmId = algorithmId;
            IndicatorId = indicatorId;
            OperatorId = operatorId;
            Active = active;
            Parameter = parameter;
            Tags= tags;
            EntityRules = entityRules;
            Pinned = pinned;
        }

       
    }
}
