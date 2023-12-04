using Viabilidade.Domain.Models.QueryParams.Enums;

namespace Viabilidade.Domain.Models.QueryParams.Rule
{
    public class RuleQueryParams : PaginationQueryParams
    {
        public eRegraAlertaGroupBy GroupBy { get; set; } = eRegraAlertaGroupBy.Rule;
        public eRegraAlertaOrderCollumn OrderCollumn { get; set; } = eRegraAlertaOrderCollumn.LastChange;
        public eSortOrder SortOrders { get; set; } = eSortOrder.Desc;
        public IEnumerable<int> Entities { get; set; }
        public IEnumerable<int> Tags { get; set; }
        public IEnumerable<int> Channels { get; set; }
        public IEnumerable<int> Indicators { get; set; }
        public IEnumerable<int> Algorithms { get; set; }
        public IEnumerable<Guid> Editors { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public bool? Active { get; set; }
        public bool? Pinned { get; set; } = false;
    }

    public enum eRegraAlertaOrderCollumn
    {
        Id,
        RuleName,
        LastChange,
        EntitiesQuantity,
        AlertsQuantity,
        TreatmentsQuantity,
        PercentageTreatment,
        EntityId,
        EntityName,
        RulesQuantity
    }

    public enum eRegraAlertaGroupBy
    {
        Rule,
        Entity
    }
}
