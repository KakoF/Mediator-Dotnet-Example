using MediatR;
using Viabilidade.Domain.Exceptions;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Interfaces.Services.UseCases.Rule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Application.Commands.Alert.Rule.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateRuleRequest, RuleModel>
    {
        private readonly IUpdateRuleService _updateRuleService;
        private readonly IRuleService _ruleService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommandHandler(IUpdateRuleService updateRuleService, IRuleService ruleService, IUnitOfWork unitOfWork)
        {
            _updateRuleService = updateRuleService;
            _ruleService = ruleService;
            _unitOfWork = unitOfWork;
    }
        public async Task<RuleModel> Handle(UpdateRuleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var oldRule = await _ruleService.PreviewRuleActiveAsync(request.Id);
                if (oldRule == null)
                    throw new DomainException("Regra não encontrada", 404);


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
                rule.UpdateVersion(oldRule);

                var result = await _updateRuleService.UpdateAsync(request.Id, rule);
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