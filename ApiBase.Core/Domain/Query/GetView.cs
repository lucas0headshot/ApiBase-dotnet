using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBase.Core.Domain.Query
{
    public class GetView
    {
        public int Total { get; set; }
        public IList<object> Content { get; set; }
    }
}
