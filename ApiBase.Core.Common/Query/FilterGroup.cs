using System.Collections.Generic;

namespace ApiBase.Core.Common.Query
{
    public class FilterGroup
    {
        public string filter { get; set; }
        public List<FilterModel> Filters { get; set; }
        public bool And { get; set; } = true;
    }
}
