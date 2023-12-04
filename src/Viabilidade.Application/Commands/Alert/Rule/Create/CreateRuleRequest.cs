using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.Create
{
    public class CreateRuleRequest : IRequest<RuleModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AlgorithmId { get; set; }
        public int IndicatorId { get; set; }
        public int OperatorId { get; set; }
        public bool Active { get; set; }
        public bool Pinned { get; set; }
        public CreateParameterRequest Parameter { get; set; }
        public IEnumerable<CreateTagsRequest> Tags { get; set; }
        public IEnumerable<CreateEntityRuleRequest> EntityRules { get; set; }
    }

    public class CreateTagsRequest
    {
        public int Id { get; set; }
    }

    public class CreateParameterRequest
    {
        public decimal LowSeverity { get; set; }
        public decimal MediumSeverity { get; set; }
        public decimal HighSeverity { get; set; }
        public decimal EvaluationPeriod { get; set; }
        public decimal? ComparativePeriod { get; set; }
    }

    public class CreateEntityRuleRequest
    {
        public int EntityId { get; set; }
        public bool Active { get; set; }
        public int? ChannelId { get; set; }
        public CreateParameterRequest Parameter { get; set; }
    }
}
