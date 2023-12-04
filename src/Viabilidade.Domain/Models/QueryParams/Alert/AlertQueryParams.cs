using Viabilidade.Domain.Models.QueryParams.Enums;

namespace Viabilidade.Domain.Models.QueryParams.Alert
{
    public class AlertQueryParams : PaginationQueryParams
    {
        public eAlertaGeradoCollumn OrderCollumn { get; set; } = eAlertaGeradoCollumn.Date;
        public eSortOrder SortOrders { get; set; } = eSortOrder.Desc;
        public IEnumerable<int> Squads { get; set; }
        public IEnumerable<int> Entities { get; set; }
        public IEnumerable<Guid> Responsibles { get; set; }
        public IEnumerable<int> Tags { get; set; }
        public IEnumerable<int> Channels { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public bool? Active { get; set; } = true;
        public bool? Pinned { get; set; } = false;
    }
   
    public enum eAlertaGeradoCollumn
    {
        Id,
        EntityName,
        ChannelName,
        AlertName,
        Value,
        AlertsQuantity,
        Severity,
        Date
    }
}
