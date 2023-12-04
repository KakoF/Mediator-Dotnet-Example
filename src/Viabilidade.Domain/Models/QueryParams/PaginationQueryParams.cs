namespace Viabilidade.Domain.Models.QueryParams
{
    public class PaginationQueryParams : FilterSearch
    {
        public int Page { get; set; } = 1;
        public int TotalPage { get; set; } = 10;
    }
}
