using MediatR;
using Viabilidade.Domain.Interfaces.Services.UseCases.Rule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Application.Commands.Alert.Rule.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateRuleRequest, RuleModel>
    {
        private readonly ICreateRuleService _createRuleService;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommandHandler(ICreateRuleService createRuleService, IUnitOfWork unitOfWork)
        {
            _createRuleService = createRuleService;
            _unitOfWork = unitOfWork;
        }
        public async Task<RuleModel> Handle(CreateRuleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parameter = new ParameterModel(true, request.Parameter.HighSeverity, request.Parameter.MediumSeverity, request.Parameter.LowSeverity, request.Parameter.ComparativePeriod, request.Parameter.EvaluationPeriod);
                var rule = new RuleModel(request.Name, request.Description, request.AlgorithmId, request.IndicatorId, request.OperatorId, request.Active, request.Pinned);
                var tags = new List<TagAlertModel>();
                foreach (var tag in request.Tags)
                {
                    tags.Add(new TagAlertModel(tag.Id, true));
                }

                var ruleEntities = new List<EntityRuleModel>();

                foreach (var entityRule in request.EntityRules)
                {
                    ParameterModel parameterEntityRule = null;
                    if (entityRule.Parameter != null)
                        parameterEntityRule = new ParameterModel(true, entityRule.Parameter.HighSeverity, entityRule.Parameter.MediumSeverity, entityRule.Parameter.LowSeverity, entityRule.Parameter.ComparativePeriod, entityRule.Parameter.EvaluationPeriod);

                    ruleEntities.Add(new EntityRuleModel(entityRule.EntityId, entityRule.Active, entityRule.ChannelId, parameterEntityRule));
                }

                rule.Parameter = parameter;
                rule.TagAlerts = tags;
                rule.EntityRules = ruleEntities;
                rule.NewVersion();

                _unitOfWork.BeginTransaction();
                var result = await _createRuleService.CreateAsync(rule);
                _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }


    }
}