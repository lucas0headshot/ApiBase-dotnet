namespace CoreBackend.src.DTOs
{
    public class QueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string? OrderBy { get; set; }
        public string? Direction { get; set; } = "asc";
        public Dictionary<string, string>? Filters { get; set; } = new();
        public List<string>? Fields { get; set; }
    }
}
