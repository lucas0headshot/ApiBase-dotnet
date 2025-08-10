namespace ApiBase.Core.Domain.DTOs
{
    public class QueryExecutionDTO
    {
        public IQueryable<object> Data { get; set; }
        public int Total { get; set; }
    }
}
