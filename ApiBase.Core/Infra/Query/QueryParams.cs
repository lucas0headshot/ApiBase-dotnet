using ApiBase.Core.Domain.Query;
using Newtonsoft.Json;

namespace ApiBase.Core.Infra.Query
{
    public class QueryParams : QueryField
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
                throw new Exception("Error deserializing 'includes'.");
            }
        }

        public static QueryParams FilterById(Guid id)
        {
            return new QueryParams
            {
                limit = 1,
                page = 1,
                start = 0,
                filter = $"[{{'property':'Id','operator':'equal','value':'{id}'}}]"
            };
        }

        public List<FilterGroup> GetFilters()
        {
            if (filter == null)
                return null;

            try
            {
                var groups = JsonConvert.DeserializeObject<List<FilterGroup>>(filter);
                if (groups.Count == 0 || groups.FirstOrDefault()?.filter == null)
                    throw new InvalidOperationException();

                foreach (var group in groups)
                {
                    group.Filters = JsonConvert.DeserializeObject<List<FilterModel>>(group.filter);
                }

                return groups;
            }
            catch (InvalidOperationException)
            {
                var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filter);
                return new List<FilterGroup>
                {
                    new FilterGroup
                    {
                        Filters = filters
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deserializing filter string: '{filter}'. Details: {ex.Message}");
            }
        }

        public List<SortModel> GetSort()
        {
            if (sort == null)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<List<SortModel>>(sort);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deserializing sort string: '{sort}'. Details: {ex.Message}");
            }
        }
    }
}
