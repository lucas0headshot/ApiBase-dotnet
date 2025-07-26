namespace ApiBase.Core.src.Infra.Query
{
    public class FilterGroup
    {
        public string filter { get; set; }
        public List<FilterModel> Filters { get; set; }
        public bool And { get; set; } = true;
    }
}
