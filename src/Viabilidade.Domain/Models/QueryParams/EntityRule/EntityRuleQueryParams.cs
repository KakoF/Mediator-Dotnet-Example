using Viabilidade.Domain.Models.QueryParams.Enums;

namespace Viabilidade.Domain.Models.QueryParams.EntityRule
{
    public class EntityRuleQueryParams : PaginationQueryParams
    {
        public int? ChannelId { get; set; }
        public eAlertaGeradoCollumn OrderCollumn { get; set; } = eAlertaGeradoCollumn.Id;
        public eSortOrder SortOrders { get; set; } = eSortOrder.Desc;
    }
   
    public enum eAlertaGeradoCollumn
    {
        Id,
        Name,
        AlertsQuantity,
        TreatmentsQuantity,
        PercentageTreatment,
    }
}
