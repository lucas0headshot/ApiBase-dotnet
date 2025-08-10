using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBase.Core.Domain.Query
{
    public class FilterGroup
    {
        public string filter { get; set; }
        public List<FilterModel> Filters { get; set; }
        public bool And { get; set; } = true;
    }
}
