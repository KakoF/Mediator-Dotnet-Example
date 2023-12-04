using System.Data;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Interfaces.Services.Host;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.EntityRule;

namespace Viabilidade.Service.Services.Alert
{
    public class EntityRuleService : IEntityRuleService
    {
        private readonly IEntityRuleRepository _regraEntidadeRepository;
        private readonly IUserService _userService;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IEntityRepository _entityRepository;
        public EntityRuleService(IEntityRuleRepository regraEntidadeRepository, IUserService userService, ITreatmentRepository treatmentRepository, IAlertRepository alertRepository, IEntityRepository entityRepository)
        {
            _regraEntidadeRepository = regraEntidadeRepository;
            _userService = userService;
            _treatmentRepository = treatmentRepository;
            _alertRepository = alertRepository;
            _entityRepository = entityRepository;
        }
        public async Task<EntityRuleModel> CreateAsync(EntityRuleModel model)
        {
            return await _regraEntidadeRepository.CreateAsync(model);
        }

        public async Task<EntityRuleModel> GetAsync(int id)
        {
            return await _regraEntidadeRepository.GetAsync(id);
        }

        public async Task<EntityRuleModel> PreviewAsync(int id)
        {
            return await _regraEntidadeRepository.PreviewAsync(id);
        }

        public async Task<EntityRuleModel> PreviewAsync(int regraId, int entidadeId)
        {
            return await _regraEntidadeRepository.PreviewAsync(regraId, entidadeId);
        }

        public async Task<EntityRuleModel> UpdateAsync(int id, EntityRuleModel model)
        {
            return await _regraEntidadeRepository.UpdateAsync(id, model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _regraEntidadeRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteByRuleAsync(int ruleId)
        {
            return await _regraEntidadeRepository.DeleteByRuleAsync(ruleId);
        }

        public async Task<IEnumerable<EntityRuleModel>> GetByRuleAsync(int ruleId)
        {
            var models = await _regraEntidadeRepository.GetByRuleAsync(ruleId);

            foreach (var entityRule in models)
            {
                entityRule.Entity = await _entityRepository.GetByIdAsync((int)entityRule.EntityId);
                entityRule.UserName = await _userService.GetUserNameAsync((Guid)entityRule.UserId);
            }
            return models;
        }

        public async Task<IEnumerable<EntityRuleModel>> GetByEntityAsync(int entityId)
        {
            var models = await _regraEntidadeRepository.GetByEntityAsync(entityId);
            foreach (var entityRule in models)
            {
                entityRule.Entity = await _entityRepository.GetByIdAsync((int)entityRule.EntityId);
                entityRule.UserName = await _userService.GetUserNameAsync((Guid)entityRule.UserId);
            }
            return models;
        }

        public async Task<PaginationModel<EntityRuleGroupByRuleDto>> GroupByRuleAsync(int ruleId, EntityRuleQueryParams queryParams)
        {
            var data = await _regraEntidadeRepository.GroupByRuleAsync(ruleId, queryParams);
            foreach (var entityRule in data)
            {
                entityRule.Item1.UserName = await _userService.GetUserNameAsync(entityRule.Item1.UserId);
            }

            return new PaginationModel<EntityRuleGroupByRuleDto>(data.Select(c => c.Item2).FirstOrDefault(), queryParams.Page, queryParams.TotalPage, data.Select(c => c.Item1).ToList());
        }

        public async Task<PaginationModel<EntityRuleGroupByEntityDto>> GroupByEntityAsync(int entityId, EntityRuleQueryParams queryParams)
        {
            var data = await _regraEntidadeRepository.GroupByEntityAsync(entityId, queryParams);
            foreach (var entityRule in data)
            {
                entityRule.Item1.UserName = await _userService.GetUserNameAsync(entityRule.Item1.UserId);
            }
            return new PaginationModel<EntityRuleGroupByEntityDto>(data.Select(c => c.Item2).FirstOrDefault(), queryParams.Page, queryParams.TotalPage, data.Select(c => c.Item1).ToList());

        }

        public async Task<(bool, string)> RuleEntityApplicant(int entityRuleId, int alertsQuantity, int treatmentsQuantity, decimal percentageTreatment)
        {

            if (await AderenciaPorTratativa(entityRuleId, treatmentsQuantity, percentageTreatment))
                return (true, "Aderência por tratativas (tratativas entre maior 50%) e problemas menor que 50%");

            if (await AderenciaPorAlerta(entityRuleId))
                return (true, "Aderência por disparos, menos de 30 dias com disparos");

            if (await AderenciaPorCriticidade(entityRuleId, alertsQuantity, percentageTreatment))
                return (true, "Aderência por criticidade baixa menor/igual que 40% e tratativas maior que 60%");

            return (false, null);

        }

        private async Task<bool> AderenciaPorTratativa(int entityRuleId, int treatmentsQuantity, decimal percentageTreatment)
        {
            if (percentageTreatment > 50)
            {
                var totalProblems = await _treatmentRepository.CountByEntityRuleWasProblemGroupAsync(entityRuleId);
                return ((double)totalProblems / treatmentsQuantity) < 0.5;
            }
            return false;
        }
        private async Task<bool> AderenciaPorAlerta(int entityRuleId)
        {
            var dataLastAlert = (await _alertRepository.GetLastByEntityRuleAsync(entityRuleId))?.Version;
            if (dataLastAlert == null)
                return false;
            if ((int)dataLastAlert.Value.Subtract(DateTime.Now).TotalDays < 30)
                return true;

            return false;
        }

        private async Task<bool> AderenciaPorCriticidade(int entityRuleId, int alertsQuantity, decimal percentageTreatment)
        {
            var totalLowSeverity = await _alertRepository.CountLowSeverityByEntityRuleAsync(entityRuleId);
            var severityPercente = (double)totalLowSeverity / alertsQuantity;
            if (severityPercente <= 0.4 && percentageTreatment >= 60)
                return true;

            return false;
        }
    }
}
