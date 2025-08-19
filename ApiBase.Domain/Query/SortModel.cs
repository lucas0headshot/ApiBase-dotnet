namespace ApiBase.Domain.Query
{
    public class SortModel
    {
        public object filterValue;
        public string property { get; set; }
        public string direction { get; set; }
        public bool ASC()
        {
            return direction.ToLower() == "asc";
        }
    }
}
