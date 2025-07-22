namespace Core.DTOs
{
    public class QueryParams
    {
        public int Page { get; set; } = 1;
        public int Start { get; set; } = 0;
        public int Limit { get; set; } = 25;

        public List<SortParam>? Sort { get; set; }
        public List<FilterParam>? Filter { get; set; }
    }

    public class SortParam
    {
        public string Property { get; set; } = string.Empty;
        public string Sort { get; set; } = "ASC";
    }

    public class FilterParam
    {
        public string Property { get; set; } = string.Empty;
        public string Operator { get; set; } = "equal"; 
        public object? Value { get; set; }
    }
}
