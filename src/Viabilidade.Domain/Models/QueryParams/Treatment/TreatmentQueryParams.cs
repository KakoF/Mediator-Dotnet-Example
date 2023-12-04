using Viabilidade.Domain.Models.QueryParams.Enums;

namespace Viabilidade.Domain.Models.QueryParams.Treatment
{
    public class TreatmentQueryParams : PaginationQueryParams
    {
        public eAlertaGeradoTratativaCollumn OrderCollumn { get; set; } = eAlertaGeradoTratativaCollumn.Date;
        public eSortOrder SortOrders { get; set; } = eSortOrder.Desc;
        public IEnumerable<int> Squads { get; set; }
        public IEnumerable<int> Entities { get; set; }
        public IEnumerable<Guid> Responsibles { get; set; }
        public IEnumerable<int> Tags { get; set; }
        public IEnumerable<int> Channels { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
    }
   
    public enum eAlertaGeradoTratativaCollumn
    {
        Id,
        EntityName,
        ChannelName,
        SquadName,
        RuleName,
        TreatmentsQuantity,
        LastClassName,
        PercentageProblem,
        Date,
    }
}
