using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ApiBase.Core.Infra.Query
{
    public class QueryParams
    {
        public int? page { get; set; }
        public int? start { get; set; }
        public int? limit { get; set; }
        public string? sort { get; set; }
        public string? filter { get; set; }
        public string? includes { get; set; }
        public List<string> GetIncludes()
        {
            if (includes == null)
            {
                return new List<string>();
            }

            try
            {
                return JsonConvert.DeserializeObject<List<string>>(includes);
            }
            catch (Exception)
            {
                throw new Exception("Error undoing.");
            }
        }
    }
}
