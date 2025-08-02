using System.Linq;

namespace ApiBase.Core.Common.DTOs
{
    public class QueryExecutionDTO
    {
        public IQueryable<object> Data { get; set; }
        public int Total { get; set; }
    }
}
