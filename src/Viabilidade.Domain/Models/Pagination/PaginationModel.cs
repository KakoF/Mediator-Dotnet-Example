namespace Viabilidade.Domain.Models.Pagination
{
    public class PaginationModel<T>
    {
        public PaginationModel(int totalRecords, int pageNumber, int pageSize, IEnumerable<T> data)
        {
            TotalRecords = totalRecords;
            TotalPages = totalRecords > 0 ? (int)Math.Ceiling((decimal)(totalRecords) / data.Count()) : 0;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            
        }

        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
