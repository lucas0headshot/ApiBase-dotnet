using Newtonsoft.Json;

namespace ApiBase.Core.Infra.Query
{
    public class QueryField
    {
        public string? Fields { get; set; }
        public List<string> GetFields()
        {
            if (string.IsNullOrWhiteSpace(Fields))
                return [];

            try
            {
                return JsonConvert.DeserializeObject<List<string>>(Fields) ?? [];
            }
            catch
            {
                return [];
            }
        }
    }
}
