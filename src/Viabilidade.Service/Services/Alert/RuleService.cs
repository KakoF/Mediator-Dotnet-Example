using Viabilidade.Domain.DTO.Rule;
using Viabilidade.Domain.Extensions;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Rule;
using Viabilidade.Domain.Notifications;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Service.Services.Alert
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _regraAlertaRepository;
        private readonly ITagAlertRepository _alertaTagRepository;
        private readonly IEntityRuleService _regraEntidadeService;
        private readonly IFavoriteAlertRepository _favoriteAlertRepository;
        private readonly INotificationHandler<Notification> _notification;
        private readonly IUnitOfWork _unitOfWork;
        public RuleService(IRuleRepository regraAlertaRepository, ITagAlertRepository alertaTagRepository, IEntityRuleService regraEntidadeService, IFavoriteAlertRepository favoriteAlertRepository, INotificationHandler<Notification> notification, IUnitOfWork unitOfWork)
        {
            _regraAlertaRepository = regraAlertaRepository;
            _alertaTagRepository = alertaTagRepository;
            _regraEntidadeService = regraEntidadeService;
            _favoriteAlertRepository = favoriteAlertRepository;
            _notification = notification;
            _unitOfWork = unitOfWork;
        }

        public async Task<RuleModel> ActiveInactiveAsync(int id, bool active)
        {
            var regra = await _regraAlertaRepository.GetAsync(id);
            if (regra == null)
            {
                _notification.AddNotification(404, "Regra não encontrada");
                return null;
            }
            regra.Active = active;
            regra.LastUpdateDate = DateTime.Now;
            return await _regraAlertaRepository.UpdateAsync(id, regra);
        }

        public async Task<RuleModel> CreateAsync(RuleModel model)
        {
            return await _regraAlertaRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _regraAlertaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RuleModel>> GetAsync()
        {
            return await _regraAlertaRepository.GetAsync();
        }

        public async Task<PaginationModel<RuleGroupDto>> GroupAsync(RuleQueryParams queryParams)
        {
            var data = await ParseData(queryParams);
            foreach (var item in data)
            {
                item.Item1.RuleTags = await _alertaTagRepository.GetByRuleAsync(item.Item1.Id);
            }
            return new PaginationModel<RuleGroupDto>(data.Select(c => c.Item2).FirstOrDefault(), queryParams.Page, queryParams.TotalPage, data.Select(c => c.Item1).ToList());
        }

        public async Task<RuleModel> GetAsync(int id)
        {
            return await _regraAlertaRepository.GetAsync(id);
        }

        public async Task<RuleModel> PreviewAsync(int id)
        {
            var model = await _regraAlertaRepository.PreviewAsync(id);
            if (model == null)
            {
                _notification.AddNotification(404, "Regra não encontrada");
                return null;
            }
            model.Pinned = await _favoriteAlertRepository.ExistFavoriteAsync(id);
            model.EntityRules = await _regraEntidadeService.GetByRuleAsync(model.Id);
            return model;
        }


        public async Task<RuleModel> PreviewRuleActiveAsync(int id)
        {
            var model = await _regraAlertaRepository.PreviewRuleActiveAsync(id);
            if (model == null)
            {
                _notification.AddNotification(404, "Regra não encontrada");
                return null;
            }
            model.Pinned = await _favoriteAlertRepository.ExistFavoriteAsync(id);
            model.EntityRules = await _regraEntidadeService.GetByRuleAsync(model.Id);
            return model;
        }



        public async Task<RuleModel> UpdateAsync(int id, RuleModel model)
        {
            return await _regraAlertaRepository.UpdateAsync(id, model);
        }

        private async Task<IEnumerable<Tuple<RuleGroupDto, int>>> ParseData(RuleQueryParams queryParams)
        {
            switch (queryParams.GroupBy)
            {
                case eRegraAlertaGroupBy.Rule:
                    return await _regraAlertaRepository.GroupByRuleAsync(queryParams);
                case eRegraAlertaGroupBy.Entity:
                    return await _regraAlertaRepository.GroupByEntityAsync(queryParams);
                default:
                    return await _regraAlertaRepository.GroupByRuleAsync(queryParams);

            }
        }

        public async Task SetRootRuleToRulesAsync()
        {
            try
            {
                var rules = await _regraAlertaRepository.GetRulesAsRootRuleIsNullAsync();
                _unitOfWork.BeginTransaction();
                
                foreach (var rule in rules)
                {
                    rule.RootRule = rule.LastUpdateDate.Value.DateToCode();
                    await _regraAlertaRepository.UpdateRootRuleAsync(rule.Id, rule.RootRule);
                }
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
