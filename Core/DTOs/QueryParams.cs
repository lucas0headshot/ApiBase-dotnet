namespace Core.DTOs
{
    public class QueryParams
    {
        public string Filter { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; } 
        public int Start { get; set; }
        public int Limit { get; set; }
    }
}
