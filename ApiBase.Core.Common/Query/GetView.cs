using System.Collections.Generic;

namespace ApiBase.Core.Common.Query
{
    public class GetView
    {
        public int Total { get; set; }
        public IList<object> Content { get; set; }
    }
}
